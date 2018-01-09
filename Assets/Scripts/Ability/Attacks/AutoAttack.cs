using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : Ability
{
    public AutoAttack(Entity owner)
        : base(owner)
    {
        Name = "Attack";
        CastAdd = 0;
        PowerCoefficient = 1.0f;
    }

    protected override void Do()
    {
        Target.TakeDamage(TotalPower);
        base.Do();
    }
}
