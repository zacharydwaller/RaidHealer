using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss : Entity
{
    public bool IsEnraged = false;
    public float EnrageDelay = 60;
    public float EnrageTime;

    public float TankSwapDelay = 10;
    public float TankSwapTime = 0;

    protected int currentTank = 0;

    protected List<Ability> AbilityList;

    public Boss(BattleManager mgr)
        : base(mgr)
    {
        AbilityList = new List<Ability>();
        AbilityList.Add(new AutoAttack(this));

        CurrentAbility = AbilityList[0];
    }

    public override void Tick()
    {
        foreach(var ability in AbilityList)
        {
            ability.Tick();
        }

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
            GCDFinish += GlobalCooldown;
            DoAbility();
            SelectAbility();
        }
    }

    public void SelectAbility()
    {
        for (int i = AbilityList.Count - 1; i >= 0; i--)
        {
            if(AbilityList[i].CooldownRemaining == 0)
            {
                CurrentAbility = AbilityList[i];
                break;
            }
        }
    }

    public void DoAbility()
    {
        var target = Mgr.Raid.GetTank(currentTank);

        if (target == null) target = Mgr.Raid.GetNextAlive();

        if (target != null)
        {
            CurrentAbility.Do(target);
        }
    }

    /// <summary>
    /// Increase damage by 500%, attack speed by 150%
    /// </summary>
    public void Enrage()
    {
        Mgr.LogAction(this, "enrages!");

        IsEnraged = true;
        AbilityPower = Numbers.IncreaseByPercent(AbilityPower, 500.0f);

        AbilityList.Clear();
        AbilityList.Add(new AutoAttack(this));
        CurrentAbility = AbilityList[0];

        float attacksPerSec = 1.0f / GlobalCooldown;
        attacksPerSec = Numbers.IncreaseByPercent(attacksPerSec, 150.0f);
        GlobalCooldown = 1.0f / attacksPerSec;
    }
}
