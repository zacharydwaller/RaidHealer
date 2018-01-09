using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal(Entity owner = null)
        : base(owner)
    {
        Name = "Heal";
        CastAdd = 0.25f;
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
        base.CancelCast();
    }

    protected override void Do()
    {
        Target.TakeHeal(TotalPower);
        RemoveHealPredict();

        base.Do();
    }
}
