using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Raid Raid;
    public int RaidIndex;

    public Text NameText;
    public Text HealthText;

    public Slider HealthBar;
    public Image HealthBarFill;

    public Slider CastBar;

    public Outline RegularOutline;
    public Outline SelectOutline;

    public bool IsSelected;

    private float UpdateInterval = 0.05f;
    private float NextUpdate = 0.0f;

    private void Start()
    {
        HealthText.enabled = false;
    }

    private void Update()
    {
        if (Time.time >= NextUpdate)
        {
            UpdateInfo();
            NextUpdate = Time.time + UpdateInterval;
        }
    }

    public void Initialize(Raid raid, int raidIndex)
    {
        Raid = raid;
        RaidIndex = raidIndex;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        Raider raider = Raid.Raiders[RaidIndex];

        NameText.text = raider.Name;

        HealthText.text = Numbers.Abbreviate(raider.Health);

        // Health Bar
        HealthBar.value = raider.Health / raider.MaxHealth;
        HealthBarFill.color = RoleUtil.GetColor(raider.Role);

        // Cast Bar
        if (!raider.IsCasting) CastBar.gameObject.SetActive(false);
        else
        {
            CastBar.gameObject.SetActive(true);
            CastBar.value = 1.0f - (raider.CastRemaining / raider.CurrentAbility.CastTime);
        }
    }

    public void Select()
    {
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
}
