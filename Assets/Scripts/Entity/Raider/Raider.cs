using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    public float ItemLevel;

    protected const float powerStd = 10;
    protected float gcdStd = 0.15f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        Name = Names.GetRandom();
        ItemLevel = Distribution.GetRandom(Mgr.PowerLevel, powerStd);

        //MaxHealth = Health = Power(BaseHP);
        //AbilityPower = GetPowerScaledValue(BaseAP);

        float haste = Mathf.Abs(Power.BaseGCD - Distribution.GetRandom(Power.BaseGCD, gcdStd));

        GlobalCooldown = Mathf.Max(GlobalCooldown - haste, Power.MinGCD);
        
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

    
}
