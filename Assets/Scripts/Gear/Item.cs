using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public SlotType SlotType;

    public float ItemLevel;

    public float PlusHealth;
    public float AbilityPower;
    public float Haste;

    public static Item CreateItem(SlotType slot, float itemLevel)
    {
        var item = new Item
        {
            Name = Slot.GetTypeString(slot),
            SlotType = slot,
            ItemLevel = itemLevel,
            PlusHealth = GetScaledStat(Power.BaseHP, itemLevel, slot),
            AbilityPower = GetScaledStat(Power.BaseAP, itemLevel, slot),
            Haste = GetScaledHaste(itemLevel, slot)
        };

        item.PlusHealth = Mathf.Round(item.PlusHealth);
        item.AbilityPower = Mathf.Round(item.AbilityPower);
        item.Haste = Mathf.Round(item.Haste * 10.0f) / 10.0f;

        return item;
    }

    private static float GetScaledStat(float baseValue, float itemLevel, SlotType slot)
    {
        return Power.ScaleValue(baseValue, itemLevel) * Power.SlotMod(slot);
    }

    private static float GetScaledHaste(float itemLevel, SlotType slot)
    {
        return Power.BaseHaste + (itemLevel * Power.HasteMult * Power.SlotMod(slot));
    }
}