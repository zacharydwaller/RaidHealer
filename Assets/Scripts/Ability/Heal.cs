using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal(Entity owner)
        : base(owner)
    {
        Name = "Heal";
        CastTime = 0.5f;
        PowerCoefficient = 1.0f;
    }

    public override void Do(Entity target, float power)
    {
        base.Do(target, power);

        target.TakeHeal(power * PowerCoefficient);
    }
}
