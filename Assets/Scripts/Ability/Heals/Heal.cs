using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal(Entity owner)
        : base(owner)
    {
        Name = "Heal";
        CastTime = 0.25f;
        PowerCoefficient = 1.0f;
    }

    public override void StartCast(Entity target, float power)
    {
        AddHealPredict(target, power);
    }

    public override void Do(Entity target, float power)
    {
        base.Do(target, power);

        target.TakeHeal(power * PowerCoefficient);

        RemoveHealPredict(target, power);
    }
}
