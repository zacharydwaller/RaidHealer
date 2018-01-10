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

    /// <summary>
    /// List of abilities. Should be ordered least-to-most priority. So add AutoAttack first.
    /// </summary>
    protected List<Ability> AbilityList;

    public Boss(BattleManager mgr)
        : base(mgr)
    {
        
    }

    public override void Tick()
    {
        base.Tick();

        // Check enrage timer
        if (IsEnraged == false && Time.time >= EnrageTime)
        {
            Enrage();
        }

        // Check tank swap
        if(Time.time >= TankSwapTime)
        {
            TankSwapTime += TankSwapDelay;
            currentTank = (currentTank + 1) % Mgr.Raid.NumTanks;
        }

        // Check if can do ability
        if(GCDReady & !IsCasting)
        {
            SelectAbility();
            DoAbility();
        }
    }

    protected override void TickAbilities()
    {
        foreach (var ability in AbilityList)
        {
            ability.Tick();
        }
    }

    /// <summary>
    /// Iterates through AbilityList in reverse and selects the first one that's not on cooldown.
    /// </summary>
    protected override void SelectAbility()
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

    /// <summary>
    /// Starts casting selected ability. Tries to target tanks if alive. Otherwise targets next raider in line.
    /// Random Attack abilities will always target random regardless.
    /// </summary>
    protected override void DoAbility()
    {
        var target = Mgr.Raid.GetTank(currentTank);

        if (target == null) target = Mgr.Raid.GetNextAlive();
        if (target != null)
        {
            StartCast(target);
        }
    }

    /// <summary>
    /// Increase damage by 500%, attack speed by 150%.
    /// Boss will only auto attack.
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
