using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectRandom : IAbilityEffect
{
    public float PowerCoefficient { get; protected set; }

    public DamageEffectRandom(float powerCoeff)
    {
        PowerCoefficient = powerCoeff;
    }

    public void Invoke(Entity owner, Ability parent, Entity target)
    {
        var randomTarget = owner.Mgr.Raid.GetRandom();
        randomTarget.TakeDamage(owner.AbilityPower * PowerCoefficient);
    }
}
