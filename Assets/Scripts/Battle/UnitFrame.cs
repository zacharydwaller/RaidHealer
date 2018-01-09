using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public BattleManager Mgr;

    public Raider Raider;

    public Text NameText;
    public Text HealthText;

    public Slider HealthBar;
    public Image HealthBarFill;

    public Slider HealPredict;

    public Slider CastBar;

    public Image Background;

    public Outline RegularOutline;
    public Outline SelectOutline;

    public bool IsSelected;

    protected float HealthLerpConstant = 0.3f;

    private void Start()
    {
        HealthText.enabled = false;
    }

    private void Update()
    {
        UpdateInfo();
    }

    public void Initialize(BattleManager mgr, int index)
    {
        Mgr = mgr;
        Raider = Mgr.Raid.Raiders[index] as Raider;

        NameText.text = Raider.Name;

        HealthBar.maxValue = Raider.MaxHealth;
        HealthBarFill.color = RoleUtil.GetColor(Raider.Role);

        HealPredict.maxValue = Raider.MaxHealth;

        CastBar.gameObject.SetActive(false);

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        // Text
        HealthText.text = Numbers.Abbreviate(Raider.Health);

        // Health Bar
        HealthBar.value = Mathf.Lerp(HealthBar.value, Raider.Health, HealthLerpConstant);

        // Heal predict
        HealPredict.value = Mathf.Lerp(HealPredict.value, Raider.Health + Raider.HealPredict, HealthLerpConstant);

        // Cast Bar
        if (!Raider.IsCasting) CastBar.gameObject.SetActive(false);
        else
        {
            CastBar.gameObject.SetActive(true);
            CastBar.value = 1.0f - (Raider.CurrentAbility.CastRemaining / (Raider.CurrentAbility.CastTime));
        }

        // Dead state
        if (Raider.IsDead)
        {
            UnSelect();
            SetAlpha(Background, 0.1f);
            SetAlpha(NameText, 0.5f);
            HealthText.text = "Dead";
            HealPredict.gameObject.SetActive(false);
            CastBar.gameObject.SetActive(false);
        }
    }

    public void Select()
    {
        if (Raider.IsDead) return;

        IsSelected = true;
        RegularOutline.enabled = false;
        SelectOutline.enabled = true;

        Mgr.Player.SelectTarget = Raider;
    }

    public void UnSelect()
    {
        IsSelected = false;
        RegularOutline.enabled = true;
        SelectOutline.enabled = false;

        Mgr.Player.SelectTarget = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HealthText.enabled = true;

        Mgr.Player.HoverTarget = Raider;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HealthText.enabled = false;

        Mgr.Player.HoverTarget = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!IsSelected && Raider.IsAlive)
        {
            Mgr.UFManager.UnselectAll();
            Select();
        }
    }

    public void SetAlpha(MaskableGraphic element, float alpha)
    {
        Color col = element.color;
        col.a = alpha;
        element.color = col;
    }
}
