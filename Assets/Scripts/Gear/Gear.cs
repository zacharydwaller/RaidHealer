using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear
{
    public Slot Weapon;

    public Slot Head;
    public Slot Shoulders;
    public Slot Chest;

    public Slot LeftRing;
    public Slot RightRing;

    public Slot Trinket;

    public Gear()
    {
        Weapon = new Slot(SlotType.Weapon);

        Head = new Slot(SlotType.Head);
        Shoulders = new Slot(SlotType.Shoulders);
        Chest = new Slot(SlotType.Chest);

        LeftRing = new Slot(SlotType.Ring);
        RightRing = new Slot(SlotType.Ring);

        Trinket = new Slot(SlotType.Trinket);
    }

    public float GetAverageItemLevel()
    {
        float total;
        float numSlots = 7.0f;

        total =
            Weapon.ItemLevel
            + Head.ItemLevel
            + Shoulders.ItemLevel
            + Chest.ItemLevel
            + LeftRing.ItemLevel
            + RightRing.ItemLevel
            + Trinket.ItemLevel;

        return total / numSlots;
    }
}