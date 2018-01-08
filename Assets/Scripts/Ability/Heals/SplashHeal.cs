using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHeal : Ability
{
    public SplashHeal(Entity owner)
        : base(owner)
    {
        Name = "Splash Heal";
        CastTime = 0.5f;
        PowerCoefficient = 0.33f;
        Cooldown = 8.0f;
    }

    public override void StartCast(Entity target)
    {
        Targets = (List<Entity>) Owner.Mgr.Raid.GetSplash(target as Raider);
        AddHealPredict();
    }

    public override void CancelCast()
    {
        RemoveHealPredict();
    }

    public override void Do(Entity target = null)
    {
        foreach (var raider in Targets)
        {
            raider.TakeHeal(TotalPower);
        }
        RemoveHealPredict();

        base.Do();
    }
}
