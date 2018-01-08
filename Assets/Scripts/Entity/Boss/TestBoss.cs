﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    public TestBoss(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        MaxHealth = Health = 5 * Numbers.Million;
        AbilityPower = 2500;
        GlobalCooldown = 2.0f;
        EnrageDelay = 60;
        EnrageTime = Time.time + EnrageDelay;
        CurrentAbility = new AutoAttack(this);
    }
}
