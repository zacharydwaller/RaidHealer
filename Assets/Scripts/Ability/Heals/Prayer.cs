using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : HealAbility
{
    public Prayer(Entity owner = null)
        : base(owner)
    {
        Name = "Prayer";
        CastAdd = 0.5f;
        PowerCoefficient = 0.33f;
        Cooldown = 8.0f;
    }

    public override void StartCast(Entity target)
    {
        base.StartCast(null);
        TargetList = (List<Entity>)Owner.Mgr.Raid.GetSplash(target as Raider);
        AddHealPredict();
    }

    protected override void Do()
    {
        foreach (var raider in TargetList)
        {
            raider.TakeHeal(TotalPower);
        }

        base.Do();
    }
}
