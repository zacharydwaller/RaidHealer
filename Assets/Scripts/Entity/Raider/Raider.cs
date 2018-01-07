using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    public float AbilityPower = 1000;

    protected float baseHealth = 10000;
    protected float healthStdDev = 500;

    protected float baseAP = 1000;
    protected float apStdDev = 50;

    protected float baseAD = 1.25f;
    protected float adStdDev = 0.10f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        Health = Distribution.GetRandom(baseHealth, healthStdDev);
        AbilityPower = Distribution.GetRandom(baseAP, apStdDev);
        GlobalCooldown = Distribution.GetRandom(baseAD, adStdDev);

        GCDFinish = GlobalCooldown;

        CurrentAbility = new AutoAttack();
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
        //CurrentAbility.Do(Mgr.Boss, AbilityPower);
        Mgr.Boss.TakeDamage(AbilityPower);
    }
}
