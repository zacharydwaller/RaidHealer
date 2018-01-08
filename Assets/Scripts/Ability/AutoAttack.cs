using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : Ability
{
    public AutoAttack(Entity owner)
        : base(owner)
    {
        Name = "Attack";
        CastTime = 0;
        PowerCoefficient = 1.0f;
    }

    public override void Do(Entity target, float power)
    {
        base.Do(target, power);

        target.TakeDamage(power * PowerCoefficient);
    }
}
