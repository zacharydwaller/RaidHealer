using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Raidwide CD, applies hot
/// </summary>
public class Supplication : Ability
{
    public Supplication()
    {
        Name = "Supplication";
        ImagePath = "Image/Cleric/supplication";

        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(0.5f),
            new ApplyAuraEffectRaidwide(new SupplicationHot())
        };

        ManaCost = 1000;
        CastTime = 2.0f;
        Cooldown = 30.0f;
    }
}

public class SupplicationHot : HotEffect
{
    public SupplicationHot()
        : base()
    {
        Name = "Supplication";
        ImagePath = "Image/Cleric/supplication";
        PowerCoefficient = 2.0f;
        Duration = 8.0f;
        TickDelay = 2.0f;
    }
}