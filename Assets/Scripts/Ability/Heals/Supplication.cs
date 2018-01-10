using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplication : HealAbility
{
    int TargetCount;

    public Supplication(Entity owner = null)
        : base(owner)
    {
        Name = "Supplication";
        CastAdd = 0.5f;
        PowerCoefficient = 0.5f;
        Cooldown = 12.0f;
        ImagePath = "Image/Cleric/supplication";
        TargetCount = 6;
    }

    public override void StartCast(Entity target)
    {
        TargetList = (List<Entity>)Owner.Mgr.Raid.GetSmartAoE(TargetCount);
        base.StartCast(null);
    }
}
