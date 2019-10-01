using UnityEngine;

public class Player : Raider
{
    public Raider HoverTarget;
    public Raider SelectTarget;

    protected const float QueueTime = 0.5f;

    public Player(BattleManager mgr, PlayerInfo info)
        : base(mgr)
    {
        Name = info.Name;
        ItemLevel = info.Gear.AverageItemLevel;
        Role = Role.Healer;

        MaxHealth = Health = Power.BaseHP + info.Gear.TotalPlusHP;
        AbilityPower = Power.BaseAP + info.Gear.TotalAbilityPower;
        GlobalCooldown = Power.GetHastedGCD(info.Gear.TotalHaste);

        Abilities = info.AbilityList;
    }

    public override void Tick()
    {
        base.Tick();

        // Get Escape input
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if(CastManager.IsCasting)
            {
                CastManager.CancelCast();
            }
            else
            {
                Mgr.UFManager.UnselectAll();

                // Pause
            }
        }

        // Cast queued ability
        if(CastManager.ReadyToCast && QueuedAbility != null)
        {
            AbilityPressed(QueuedAbility);
            QueuedAbility = null;
        }

        // Clear target
        if(Input.GetMouseButtonDown(1))
        {
            Mgr.UFManager.UnselectAll();
        }
    }

    public void AbilityPressed(Ability ability)
    {
        // Do ability if ready
        if (CastManager.ReadyToCast)
        {
            QueuedAbility = ability;
            DoAbility();
        }
    }

    public void AbilityPressed(int index)
    {
        if (index < 0 || index >= Abilities.Count) return;

        var ability = Abilities[index];

        // Do ability if ready
        if(CastManager.ReadyToCast)
        {
            QueuedAbility = ability;
            DoAbility();
        }
        // Queue ability if within queue time
        else if(CastManager.GCDFinish - Time.time <= QueueTime
            || CastManager.CastFinishTime - Time.time <= QueueTime)
        {
            QueuedAbility = ability;
        }
    }

    protected override void DoAbility()
    {
        Entity target;

        if (HoverTarget != null)
        {
            target = HoverTarget;
        }
        else if (SelectTarget != null)
        {
            target = SelectTarget;
        }
        else
        {
            target = this;
        }

        if (target.IsAlive)
        {
            CastManager.StartCast(QueuedAbility, target);
        }

        QueuedAbility = null;
    }

    public Ability GetAbility(int index)
    {
        if (index < 0 || index >= Abilities.Count) return null;
        else return Abilities[index];
    }
}
