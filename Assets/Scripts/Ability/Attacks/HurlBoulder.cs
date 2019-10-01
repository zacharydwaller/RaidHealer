using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Attacks random raider, 8 second CD
/// </summary>
public class HurlBoulder : Ability
{
    public HurlBoulder()
    {
        Name = "Hurl Boulder";
        CastTime = 1.75f;
        Cooldown = 8.0f;

        TargetType = TargetType.Foe;
        Effects = new List<IAbilityEffect>()
        {
            new DamageEffectRandom(0.5f)
        };
    }
}
