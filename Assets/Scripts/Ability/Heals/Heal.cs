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

    public override void StartCast(Entity target)
    {
        base.StartCast(target);
        AddHealPredict();
    }

    public override void CancelCast()
    {
        RemoveHealPredict();
    }

    public override void Do(Entity target = null)
    {
        Targets[0].TakeHeal(TotalPower);
        RemoveHealPredict();

        base.Do();
    }
}
