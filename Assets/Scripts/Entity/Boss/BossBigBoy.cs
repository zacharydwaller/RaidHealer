using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigBoy : Boss
{
    public BossBigBoy(BattleManager mgr)
        : base(mgr)
    {
        Name = "Big Boy";
        MaxHealth = Health = 37.5f * Numbers.Thousand;
        AbilityPower = 150;
        GlobalCooldown = 2.0f;
        EnrageTime = Time.time + EnrageDelay;

        Abilities = new List<Ability>
        {
            new RainFire(),
            new Skewer(),
            new HurlBoulder(),
            new Stomp(),
        };

        // Start every ability on cooldown
        foreach(var ability in Abilities)
        {
            Cooldowns[ability.Name] = Time.time + ability.Cooldown;
        }
    }
}
