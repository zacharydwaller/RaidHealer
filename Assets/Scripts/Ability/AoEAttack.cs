using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : Ability
{
    public AoEAttack(Entity owner)
        : base(owner)
    {
        Name = "AoE";
        CastTime = 0;
        PowerCoefficient = 0.25f;
        Cooldown = 12.0f;
    }

    public override void Do(Entity target, float power)
    {
        var raid = Owner.Mgr.Raid;

        foreach (var raider in raid.Raiders)
        {
            raider.TakeDamage(power * PowerCoefficient);
            Owner.Mgr.LogAction(Owner, target, this);
        }

        CooldownRemaining = Cooldown;
    }
}
