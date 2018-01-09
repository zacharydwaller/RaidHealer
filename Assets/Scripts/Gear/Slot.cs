using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    Weapon,
    Head, Shoulders, Chest,
    Ring, Trinket
}

[System.Serializable]
public class Slot
{
    public SlotType SlotType;

    public Item Item;

    public bool IsEquipped { get { return Item != null; } }
    public float ItemLevel { get { return IsEquipped ? Item.ItemLevel : 0; } }

    public Slot(SlotType type)
    {
        SlotType = type;
    }

    public static string GetTypeString(SlotType slot)
    {
        switch (slot)
        {
            case SlotType.Weapon: return "Weapon";
            case SlotType.Head: return "Head";
            case SlotType.Chest: return "Chest";
            case SlotType.Shoulders: return "Shoulders";
            case SlotType.Trinket: return "Trinket";
            case SlotType.Ring: return "Ring";
        }
        return "";
    }
}

