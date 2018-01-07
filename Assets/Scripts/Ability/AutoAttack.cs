using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : Ability
{
    public AutoAttack()
    {
        Name = "Attack";
        CastTime = 0;
        PowerCoefficient = 1.0f;
    }

    public override void Do(Entity target, float power)
    {
        target.TakeDamage(power * PowerCoefficient);
    }
}
