using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    public float ItemLevel;

    public List<Aura> Auras;

    protected const float powerStd = 10;
    protected float gcdStd = 0.15f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        Name = Names.GetRandom();
        ItemLevel = Distribution.GetRandom(Mgr.RaidItemLevel, powerStd);

        MaxHealth = Health = Power.ScaleValue(Power.BaseHP, ItemLevel);
        AbilityPower = Power.ScaleValue(Power.BaseAP, ItemLevel);

        float haste = Mathf.Abs(Power.BaseGCD - Distribution.GetRandom(Power.BaseGCD, gcdStd));
        GlobalCooldown = Mathf.Max(GlobalCooldown - haste, Power.MinGCD);
        GCDFinish = GlobalCooldown;

        CurrentAbility = new AutoAttack(this);

        Auras = new List<Aura>();
    }

    public override void Tick()
    {
        base.Tick();

        for(int i = 0; i < Auras.Count; i++)
        {
            Auras[i].Tick();
        }

        // Ready for new cast
        if (GCDReady && !IsCasting)
        {
            SelectAbility();
            DoAbility();
        }
    }

    public void AddAura(Aura newAura)
    {
        // If aura already exists, refresh it
        foreach(var aura in Auras)
        {
            if (aura.GetType() == newAura.GetType())
            {
                aura.Start();
                return;
            }
        }

        // Otherwise add new aura
        Auras.Add(newAura);
    }
}
