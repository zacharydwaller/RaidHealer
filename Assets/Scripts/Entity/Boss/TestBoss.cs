using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    public TestBoss(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        //MaxHealth = Health = 25.0f * Numbers.Thousand;
        MaxHealth = Health = 1.0f * Numbers.Million;
        AbilityPower = 150;
        GlobalCooldown = 2.0f;
        EnrageDelay = 60;
        EnrageTime = Time.time + EnrageDelay;

        AbilityList.Add(new AttackRandom(this));
        AbilityList.Add(new Splash(this));
    }
}
