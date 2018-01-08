using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raider
{
    public Player(BattleManager mgr)
        : base(mgr)
    {
        Name = "Player";
        Role = Role.Healer;
        MaxHealth = Health = baseHealth;
        AbilityPower = baseAP;
        GlobalCooldown = baseGCD;

        GCDFinish = 0;
    }

    public override void Tick()
    {
        
    }

    public override void DoAbility()
    {
        
    }
}
