using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHeal : Ability
{
    public SplashHeal(Entity owner)
        : base(owner)
    {
        Name = "Splash Heal";
        CastAdd = 0.5f;
        PowerCoefficient = 0.33f;
        Cooldown = 8.0f;
    }

    public override void StartCast(Entity target)
    {
        base.StartCast(target);
        TargetList = (List<Entity>) Owner.Mgr.Raid.GetSplash(target as Raider);
        AddHealPredict();
    }

    public override void CancelCast()
    {
        base.CancelCast();
        RemoveHealPredict();
    }

    protected override void Do()
    {
        foreach (var raider in TargetList)
        {
            raider.TakeHeal(TotalPower);
        }
        RemoveHealPredict();

        base.Do();
    }
}
