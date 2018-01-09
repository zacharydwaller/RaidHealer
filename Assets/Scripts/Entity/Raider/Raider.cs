using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    public float PowerLevel;

    protected const float BaseAP = 20;
    protected const float BaseHP = 100;

    protected const float PowerCoef = 1.5f;
    protected const float PowerExp = 0.02f;

    protected const float powerStd = 10;

    protected float baseGCD = 1.5f;
    protected float gcdStdDev = 0.15f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        Name = Names.GetRandom();
        PowerLevel = Distribution.GetRandom(Mgr.PowerLevel, powerStd);

        MaxHealth = Health = GetPowerScaledValue(BaseHP);
        AbilityPower = GetPowerScaledValue(BaseAP);

        float haste = Mathf.Abs(baseGCD - Distribution.GetRandom(baseGCD, gcdStdDev));

        GlobalCooldown -= haste;

        GCDFinish = GlobalCooldown;

        CurrentAbility = new AutoAttack(this);
    }

    public override void Tick()
    {
        base.Tick();

        // Ready for new cast
        if (GCDReady && !IsCasting)
        {
            SelectAbility();
            DoAbility();
        }
    }

    protected float GetPowerScaledValue(float baseValue)
    {
        return baseValue * Mathf.Pow(PowerCoef, PowerLevel * PowerExp);
    }
}
