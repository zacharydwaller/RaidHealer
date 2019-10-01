using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Power
{
    public const float BaseHP = 100;
    public const float BaseAP = 20;
    public const float BaseHaste = 0;

    public const float PowerCoef = 1.5f;
    public const float PowerExp = 0.02f;

    public const float HasteMult = 0.0715f;

    public const float BaseGCD = 1.5f;
    public const float MinGCD = 1.0f;

    public static float SlotMod(SlotType slot)
    {
        switch (slot)
        {
            case SlotType.Weapon: return 0.27f;
            case SlotType.Head:
            case SlotType.Chest: return 0.18f;
            case SlotType.Shoulders:
            case SlotType.Trinket: return 0.14f;
            case SlotType.Ring: return 0.09f;
        }
        return 0;
    }

    public static float ScaleValue(float baseValue, float itemLevel)
    {
        return baseValue * Mathf.Pow(PowerCoef, itemLevel * PowerExp);
    }

    public static float GetHastedGCD(float haste)
    {
        float hasteSeconds = haste / 100.0f;
        return Mathf.Clamp(BaseGCD - hasteSeconds, MinGCD, BaseGCD);
    }

    public static float GetHastedCD(float cooldown, float haste)
    {
        float hasteSeconds = haste / 100.0f;
        return Mathf.Clamp(cooldown - hasteSeconds, 0.0f, cooldown);
    }
}
