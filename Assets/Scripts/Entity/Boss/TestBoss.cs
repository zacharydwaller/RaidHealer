﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Boss
{
    public TestBoss(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        MaxHealth = Health = 37.5f * Numbers.Thousand;
        AbilityPower = 150;
        GlobalCooldown = 2.0f;
        EnrageDelay = 60;
        EnrageTime = Time.time + EnrageDelay;

        Abilities = new List<Ability>
        {
            new AutoAttack(),
            //new AttackRandom(this),
            //new SplashAttack(this),
            //new AoEAttack(this)
        };

        // Start every ability on cooldown
        foreach(var ability in Abilities)
        {
            Cooldowns[ability.Name] = Time.time + ability.Cooldown;
        }
    }
}
