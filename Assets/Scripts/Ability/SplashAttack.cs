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

    public override void Do(Entity target, float power)
    {
        var raid = Owner.Mgr.Raid;
        target = raid.GetRandom();

        var center = raid.GetCoordinate(target as Raider);
        var raiders = raid.GetSplash(center);

        foreach(var raider in raiders)
        {
            raider.TakeDamage(power * PowerCoefficient);
            Owner.Mgr.LogAction(Owner, target, this);
        }

        CooldownRemaining = Cooldown;
    }
}
