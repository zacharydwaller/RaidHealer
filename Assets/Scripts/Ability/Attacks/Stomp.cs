using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Attacks raiders in a splash, 12 second CD
///     Center of splash takes an extra hit of damage at 50% power
/// </summary>
public class Stomp : Ability
{
    public Stomp()
    {
        Name = "Stomp";
        CastTime = 2.0f;
        Cooldown = 10.0f;

        Effects = new List<IAbilityEffect>()
        {
            new DamageEffectRandomSplash(0.50f)
        };
    }
}
