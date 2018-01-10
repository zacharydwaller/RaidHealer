using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : Ability
{
    public SplashAttack(Entity owner)
        : base(owner)
    {
        Name = "Splash";
        CastAdd = 0;
        PowerCoefficient = 0.25f;
        Cooldown = 4.0f;
    }

    protected override void Do()
    {
        var raid = Owner.Mgr.Raid;
        var center = raid.GetRandom();

        TargetList = (List<Entity>) raid.GetSplash(center);

        foreach(var raider in TargetList)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
