using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashHeal : Ability
{
    protected IList<Raider> raiders;

    public SplashHeal(Entity owner)
        : base(owner)
    {
        Name = "Splash Heal";
        CastTime = 0.5f;
        PowerCoefficient = 0.33f;
        Cooldown = 8.0f;
    }

    public override void StartCast(Entity target, float power)
    {
        var raid = Owner.Mgr.Raid;
        var center = raid.GetCoordinate(target as Raider);
        raiders = raid.GetSplash(center);

        foreach (var raider in raiders)
        {
            AddHealPredict(raider, power * PowerCoefficient);
        }
    }

    public override void CancelCast(Entity target, float power)
    {
        foreach (var raider in raiders)
        {
            RemoveHealPredict(raider, power * PowerCoefficient);
        }
    }

    public override void Do(Entity target, float power)
    {
        foreach (var raider in raiders)
        {
            raider.TakeHeal(power * PowerCoefficient);
            RemoveHealPredict(raider, power * PowerCoefficient);
            Owner.Mgr.LogAction(Owner, target, this);
        }

        CooldownRemaining = Cooldown;
    }
}
