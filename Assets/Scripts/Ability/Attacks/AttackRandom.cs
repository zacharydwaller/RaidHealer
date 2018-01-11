using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandom : Ability
{
    public AttackRandom(Entity owner)
        : base(owner)
    {
        Name = "Attack Random";
        CastAdd = 0;
        PowerCoefficient = 0.5f;
        Cooldown = 8.0f;
    }

    protected override void Do()
    {
        TargetList.Clear();
        TargetList.Add(Owner.Mgr.Raid.GetRandom());
        Target.TakeDamage(TotalPower);

        base.Do();
    }
}
