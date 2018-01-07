using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    public TestBoss(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        MaxHealth = Health = 10 * Numbers.Million;
        SwingDamage = 20000;
        EnrageDelay = 30;
        EnrageTime = Time.time + EnrageDelay;
        CurrentAbility = new AutoAttack();
    }
}
