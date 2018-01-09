using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Raider
{
    public Damage(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Damage;
    }

    protected override void DoAbility()
    {
        StartCasting(Mgr.Boss);
    }
}
