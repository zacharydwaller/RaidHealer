using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prayer : HealAbility
{
    int TargetCount;

    public Prayer(Entity owner = null)
        : base(owner)
    {
        Name = "Prayer";
        CastAdd = 0.5f;
        PowerCoefficient = 0.33f;
        Cooldown = 0f;
        ImagePath = "Image/Cleric/prayer";
        TargetCount = 3;
    }

    public override void StartCast(Entity target)
    {
        TargetList = (List<Entity>)Owner.Mgr.Raid.GetSmartChain(target as Raider, TargetCount);
        base.StartCast(null);
    }
}
