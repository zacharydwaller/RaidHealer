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
    public float SpellPower;
    public float Haste;

    public static Item CreateItem(SlotType slot, float itemLevel)
    {
        var item = new Item
        {
            Name = Slot.GetTypeString(slot),
            SlotType = slot,
            ItemLevel = itemLevel,
            PlusHealth = GetStat(Power.BaseHP, itemLevel, slot),
            SpellPower = GetStat(Power.BaseAP, itemLevel, slot),
            Haste = GetStat(Power.BaseHaste, itemLevel, slot)
        };

        item.PlusHealth = Mathf.Round(item.PlusHealth);
        item.SpellPower = Mathf.Round(item.SpellPower);
        item.Haste = Mathf.Round(item.Haste * 10.0f) / 10.0f;

        return item;
    }

    private static float GetStat(float baseValue, float itemLevel, SlotType slot)
    {
        return Power.ScaleValue(baseValue, itemLevel) * Power.SlotMod(slot);
    }
}