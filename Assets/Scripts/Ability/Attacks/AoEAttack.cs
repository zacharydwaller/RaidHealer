﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : Ability
{
    public AoEAttack(Entity owner)
        : base(owner)
    {
        Name = "AoE";
        CastAdd = 0;
        PowerCoefficient = 0.25f;
        Cooldown = 12.0f;
    }

    protected override void Do()
    {
        TargetList = (List<Entity>) Owner.Mgr.Raid.GetAoE();
        foreach (var raider in TargetList)
        {
            raider.TakeDamage(TotalPower);
        }

        base.Do();
    }
}
