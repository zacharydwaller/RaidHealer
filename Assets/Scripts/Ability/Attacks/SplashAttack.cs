using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : Ability
{
    public SplashAttack(Entity owner)
        : base(owner)
    {
        Name = "Splash";
        CastTime = 0;
        PowerCoefficient = 0.25f;
        Cooldown = 4.0f;
    }

    public override void Do(Entity target = null)
    {
        var raid = Owner.Mgr.Raid;
        var center = raid.GetRandom();

        Targets = (List<Entity>) raid.GetSplash(center);

        foreach(var raider in Targets)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
