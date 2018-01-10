using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raider
{
    public Raider HoverTarget;
    public Raider SelectTarget;

    public List<Ability> AbilityList;

    protected int QueuedAbilityIndex = -1;
    protected float QueueTime = 0.5f;

    public Player(BattleManager mgr, PlayerInfo info)
        : base(mgr)
    {
        Name = info.Name;
        ItemLevel = info.Gear.AverageItemLevel;
        Role = Role.Healer;

        MaxHealth = Health = Power.BaseHP + info.Gear.TotalPlusHP;
        AbilityPower = Power.BaseAP + info.Gear.TotalAbilityPower;
        GlobalCooldown = Power.GetHastedGCD(info.Gear.TotalHaste);
        GCDFinish = 0;

        AbilityList = info.AbilityList;

        foreach(var ability in AbilityList)
        {
            ability.Owner = this;
        }
    }

    public override void Tick()
    {
        base.Tick();

        // Get Escape input
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if(IsCasting)
            {
                CancelCast();
            }
            else
            {
                // Pause
            }

            QueuedAbilityIndex = -1;
        }

        // Cast queued ability
        if(GCDReady && !IsCasting && GetAbility(QueuedAbilityIndex) != null)
        {
            AbilityPressed(QueuedAbilityIndex);
            QueuedAbilityIndex = -1;
        }

        // Clear target
        if(Input.GetMouseButtonDown(1))
        {
            Mgr.UFManager.UnselectAll();
        }
    }

    protected override void TickAbilities()
    {
        foreach(var ability in AbilityList)
        {
            ability.Tick();
        }
    }

    public override void StartCast(Entity target)
    {
        base.StartCast(target);
        OnStartingCast();
    }

    public override void CancelCast()
    {
        base.CancelCast();
        OnCancellingCast();
    }

    public override void FinishCast()
    {
        base.FinishCast();
        OnFinishingCast();
    }

    public void AbilityPressed(int index)
    {
        if (index < 0 || index >= AbilityList.Count) return;

        var ability = AbilityList[index];

        // Do ability if ready
        if(!IsCasting && GCDReady)
        {
            CurrentAbility = ability;

            Entity target;

            if(HoverTarget != null)
            {
                target = HoverTarget;
            }
            else if(SelectTarget != null)
            {
                target = SelectTarget;
            }
            else
            {
                target = this;
            }

            StartCast(target);
        }
        // Queue ability if within queue time
        else if(GCDFinish - Time.time <= QueueTime)
        {
            QueuedAbilityIndex = index;
        }
    }

    public Ability GetAbility(int index)
    {
        if (index < 0 || index >= AbilityList.Count) return null;
        else return AbilityList[index];
    }

#region EventHandling
    public event EventHandler<CastEventArgs> StartingCast = delegate { };
    public event EventHandler<CastEventArgs> CancellingCast = delegate { };
    public event EventHandler<CastEventArgs> FinishingCast = delegate { };

    protected CastEventArgs CreateEventArgs()
    {
        return new CastEventArgs
        {
            Mgr = this.Mgr,
            Owner = this,
            Ability = CurrentAbility
        };
    }

    protected virtual void OnStartingCast()
    {
        StartingCast(this, CreateEventArgs());
    }

    protected virtual void OnCancellingCast()
    {
        CancellingCast(this, CreateEventArgs());
    }

    protected virtual void OnFinishingCast()
    {
        FinishingCast(this, CreateEventArgs());
    }

    #endregion
}
