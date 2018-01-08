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

    public override void Do(Entity target = null)
    {
        Targets = Owner.Mgr.Raid.Raiders;
        foreach (var raider in Targets)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
