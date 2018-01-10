using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHeal : HealAbility
{
    public SplashHeal(Entity owner = null)
        : base(owner)
    {
        Name = "Splash Heal";
        CastAdd = 0.5f;
        PowerCoefficient = 0.1f;
        Cooldown = 8.0f;
    }

    public override void StartCast(Entity target)
    {
        base.StartCast(null);
        TargetList = (List<Entity>) Owner.Mgr.Raid.GetSplash(target as Raider);
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
