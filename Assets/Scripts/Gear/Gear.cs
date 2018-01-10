using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear
{
    public Slot[] List;

    public Gear(float itemLevel)
    {
        List = new Slot[7];
        List[0] = new Slot(SlotType.Weapon, itemLevel);
        List[1] = new Slot(SlotType.Head, itemLevel);
        List[2] = new Slot(SlotType.Shoulders, itemLevel);
        List[3] = new Slot(SlotType.Chest, itemLevel);
        List[4] = new Slot(SlotType.Ring, itemLevel);
        List[5] = new Slot(SlotType.Ring, itemLevel);
        List[6] = new Slot(SlotType.Trinket, itemLevel);
    }

    public float AverageItemLevel
    {
        get
        {
            float total = 0;
            float numSlots = List.Length;
            foreach (var slot in List)
            {
                total += slot.ItemLevel;
            }

            return total / numSlots;
        }
        
    }

    public float TotalPlusHP
    {
        get
        {
            float total = 0;
            foreach (var slot in List)
            {
                total += slot.PlusHP;
            }

            return total;
        }
    }

    public float TotalAbilityPower
    {
        get
        {
            float total = 0;
            foreach (var slot in List)
            {
                total += slot.AbilityPower;
            }

            return total;
        }
    }

    public float TotalHaste
    {
        get
        {
            float total = 0;
            foreach (var slot in List)
            {
                total += slot.Haste;
            }

            return total;
        }
    }
}