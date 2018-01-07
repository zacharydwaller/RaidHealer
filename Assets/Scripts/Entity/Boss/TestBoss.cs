using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    public TestBoss(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        MaxHealth = Health = 1 * Numbers.Million;
        SwingDamage = 10000;
        CurrentAbility = new AutoAttack();
    }
}
