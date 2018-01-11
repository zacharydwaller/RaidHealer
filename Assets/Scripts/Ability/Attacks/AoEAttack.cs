using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : Ability
{
    public AoEAttack(Entity owner)
        : base(owner)
    {
        Name = "Rain Fire";
        CastAdd = 1.0f;
        PowerCoefficient = 0.25f;
        Cooldown = 15.0f;
    }

    protected override void Do()
    {
        TargetList.Clear();
        TargetList = (List<Entity>) Owner.Mgr.Raid.GetAoE();
        foreach (var raider in TargetList)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
