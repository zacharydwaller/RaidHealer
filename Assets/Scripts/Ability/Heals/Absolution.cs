using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Big instant cast heal with cooldown
/// </summary>
public class Absolution : Ability
{
    public Absolution()
    {
        Name = "Absolution";
        ImagePath = "Image/Cleric/absolution";

        TargetType = TargetType.Friend;

        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(4.0f)
        };

        ManaCost = 1000;
        CastTime = 0;
        Cooldown = 15f;

    }
}
