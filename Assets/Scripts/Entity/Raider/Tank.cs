using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Raider
{
    public AttuneBuff Attunement;

    public Tank(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Tank;
        MaxHealth = Health *= 4.0f;
        AbilityPower *= 0.75f;

        Attunement = new AttuneBuff(this);
    }

    protected override void DoAbility()
    {
        if (Mgr.Boss.IsAlive)
        {
            StartCast(Mgr.Boss);
        }
    }
}
