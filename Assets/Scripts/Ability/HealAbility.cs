using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    public HealAbility(Entity owner = null)
        : base(owner)
    {

    }

    public override void StartCast(Entity target = null)
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
        RemoveHealPredict();
        base.Do();
    }
}
