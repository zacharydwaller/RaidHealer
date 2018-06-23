using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : OldAbility
{
    public SplashAttack(Entity owner)
        : base(owner)
    {
        Name = "Meteor";
        CastAdd = 0.01f;
        PowerCoefficient = 0.25f;
        Cooldown = 12.0f;
    }

    protected override void Do()
    {
        var raid = Owner.Mgr.Raid;
        var center = raid.GetRandom();

        TargetList.Clear();
        TargetList = (List<Entity>) raid.GetSplash(center);

        foreach(var raider in TargetList)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
