using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss : Entity
{
    public float SwingDamage;

    public float TankSwapDelay = 10;
    public float TankSwapTime = 0;

    protected int currentTank = 0;

    public Boss(BattleManager mgr)
        : base(mgr)
    {

    }

    public override void Tick()
    {
        if(Time.time >= TankSwapTime)
        {
            TankSwapTime += TankSwapDelay;
            currentTank = (currentTank + 1) % Mgr.Raid.NumTanks;
        }

        if(GCDReady & !IsCasting)
        {
            DoAbility();
        }
    }

    public void DoAbility()
    {
        GCDFinish += GlobalCooldown;

        var tank = Mgr.Raid.GetTank(currentTank);

        if(tank != null)
        {
            CurrentAbility.Do(Mgr.Raid.GetTank(currentTank), SwingDamage);
        }
        else
        {
            CurrentAbility.Do(Mgr.Raid.GetNextAlive(), SwingDamage);
        }
    }
}
