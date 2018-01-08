using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    protected float baseHealth = 1500;
    protected float healthStdDev = 250;

    protected float baseAP = 300;
    protected float apStdDev = 25;

    protected float baseGCD = 1.5f;
    protected float gcdStdDev = 0.15f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        MaxHealth = Health = Distribution.GetRandom(baseHealth, healthStdDev);
        AbilityPower = Distribution.GetRandom(baseAP, apStdDev);

        float haste = Mathf.Abs(baseGCD - Distribution.GetRandom(baseGCD, gcdStdDev));

        GlobalCooldown -= haste;

        GCDFinish = GlobalCooldown;

        CurrentAbility = new AutoAttack(this);
    }

    public override void Tick()
    {
        if(GCDReady)
        {
            GCDFinish += GlobalCooldown;
            DoAbility();
        }
    }

    public virtual void DoAbility()
    {
        if(Mgr.Boss.IsAlive)
        {
            CurrentAbility.Do(Mgr.Boss, AbilityPower);
        }
    }
}
