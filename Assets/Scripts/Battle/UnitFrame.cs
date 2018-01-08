using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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

    public void Initialize(Raid raid, int index)
    {
        Raider = raid.Raiders[index];

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
            CastBar.value = 1.0f - (Raider.CastRemaining / (Raider.GlobalCooldown + Raider.CurrentAbility.CastTime));
        }

        // Dead state
        if (Raider.IsDead)
        {
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
    }

    public void UnSelect()
    {
        IsSelected = false;
        RegularOutline.enabled = true;
        SelectOutline.enabled = false;
    }

    // Using Event handlers because OnMouseEnter doesn't work with UI elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        HealthText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HealthText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelected)
        {
            UnSelect();
        }
        else
        {
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
