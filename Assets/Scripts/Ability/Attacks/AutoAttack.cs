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

    public override void Do(Entity target)
    {
        target.TakeDamage(TotalPower);
        base.Do(target);
    }
}
