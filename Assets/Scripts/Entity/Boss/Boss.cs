using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss : Entity
{
    public float SwingDamage;

    public bool IsEnraged = false;
    public float EnrageDelay = 60;
    public float EnrageTime;

    public float TankSwapDelay = 10;
    public float TankSwapTime = 0;

    protected int currentTank = 0;

    public Boss(BattleManager mgr)
        : base(mgr)
    {

    }

    public override void Tick()
    {
        if (IsEnraged == false && Time.time >= EnrageTime)
        {
            Enrage();
        }

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

        var target = Mgr.Raid.GetTank(currentTank);

        if (target == null) target = Mgr.Raid.GetNextAlive();

        if (target != null)
        {
            CurrentAbility.Do(target, SwingDamage);
        }
    }

    /// <summary>
    /// Increase damage by 500%, attack speed by 150%
    /// </summary>
    public void Enrage()
    {
        IsEnraged = true;
        SwingDamage = Numbers.IncreaseByPercent(SwingDamage, 500.0f);

        float attacksPerSec = 1.0f / GlobalCooldown;
        attacksPerSec = Numbers.IncreaseByPercent(attacksPerSec, 150.0f);
        GlobalCooldown = 1.0f / attacksPerSec;
    }
}
