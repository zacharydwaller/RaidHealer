using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandom : Ability
{
    public AttackRandom(Entity owner)
        : base(owner)
    {
        Name = "Attack Random";
        CastTime = 0;
        PowerCoefficient = 0.5f;
        Cooldown = 6.0f;
    }

    public override void Do(Entity target = null)
    {
        target = Owner.Mgr.Raid.GetRandom();
        target.TakeDamage(TotalPower);

        base.Do(target);
    }
}
