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
        Cooldown = 4.0f;
    }

    public override void Do(Entity target, float power)
    {
        target = Owner.Mgr.Raid.GetRandom();
        target.TakeDamage(power * PowerCoefficient);

        Owner.Mgr.LogAction(Owner, target, this);
        CooldownRemaining = Cooldown;

        Coordinate cord = Owner.Mgr.Raid.GetCoordinate(target as Raider);
        Debug.Log(string.Format("Row: {0} Col: {1}", cord.Row, cord.Col));
    }
}
