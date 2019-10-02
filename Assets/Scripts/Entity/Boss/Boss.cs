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

    public float AbilityUseDelay = 3.0f;
    public float AbilityUseTime = 0;

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
        if(CastManager.ReadyToCast)
        {
            SelectAbility();
            DoAbility();
        }
    }

    /// <summary>
    /// Iterates through AbilityList in reverse and selects the first one that's not on cooldown.
    /// If all abilities on cooldown, selects AutoAttack
    /// </summary>
    protected override void SelectAbility()
    {
        if(Time.time >= AbilityUseTime)
        {
            // Find highest priority ability off cooldown
            for (int i = Abilities.Count - 1; i >= 0; i--)
            {
                if (Cooldowns[Abilities[i].Name] <= Time.time)
                {
                    QueuedAbility = Abilities[i];
                    AbilityUseTime = Time.time + AbilityUseDelay;
                    break;
                }
            }

            // Auto Attack if no abilities available
            if(QueuedAbility == null)
            {
                QueuedAbility = AutoAttack;
            }
        }
    }

    /// <summary>
    /// Starts casting selected ability. Tries to target tanks if alive. Otherwise targets next raider in line.
    /// Random Attack abilities will always target random regardless.
    /// </summary>
    protected override void DoAbility()
    {
        if (QueuedAbility == null) return;

        var target = Mgr.Raid.GetTank(currentTank);

        if (target == null) target = Mgr.Raid.GetNextAlive();
        if (target != null)
        {
            CastManager.StartCast(QueuedAbility, target);
            QueuedAbility = null;
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

        Abilities.Clear();

        float attacksPerSec = 1.0f / GlobalCooldown;
        attacksPerSec = Numbers.IncreaseByPercent(attacksPerSec, 150.0f);
        GlobalCooldown = 1.0f / attacksPerSec;
    }
}
