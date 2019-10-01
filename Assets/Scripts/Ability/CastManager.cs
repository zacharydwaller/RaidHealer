using System;
using UnityEngine;

public class CastManager
{
    public Ability CurrentAbility { get; private set; }
    public bool IsCasting { get => CurrentAbility != null; }
    
    public float CastFinishTime { get; private set; }
    public float CastRemaining { get => IsCasting ? CastFinishTime - Time.time : 0; }
    public float CastProgress { get => IsCasting ? 1.0f - (CastRemaining / CurrentAbility.CastTime) : 0; }

    public float GCDFinish { get; private set; }
    public float GCDProgress { get => GCDReady ? 1.0f : 1.0f - (Mathf.Max(GCDFinish - Time.time, 0) / Owner.GlobalCooldown); }
    public bool GCDReady { get => !Owner.IsDead && (Time.time >= GCDFinish); }

    public bool ReadyToCast { get => GCDReady && !IsCasting; }

    public Entity Owner { get; private set; }
    public Entity Target { get; private set; }

    public CastManager(Entity owner)
    {
        Owner = owner;
        CurrentAbility = null;
    }

    public void Tick()
    {
        if (IsCasting)
        {
            if (Target.IsDead)
            {
                CancelCast();
                return;
            }

            if (Time.time >= CastFinishTime)
            {
                FinishCast();
            }
        }
    }

    /// <summary>
    ///     Triggers the GCD and starts the cast
    /// </summary>
    /// <param name="target"></param>
    public void StartCast(Ability ability, Entity target)
    {
        Target = target;

        if (!Owner.Cooldowns.ContainsKey(ability.Name)) Owner.Cooldowns[ability.Name] = 0.0f;
        if (Owner.Cooldowns[ability.Name] <= Time.time)
        {
            CurrentAbility = ability;
            GCDFinish = Time.time + Owner.GlobalCooldown;
            CastFinishTime = Time.time + CurrentAbility.CastTime; // TODO: Factor in haste

            OnStartingCast(CreateEventArgs());
        }
    }

    /// <summary>
    ///     Nulls the current ability
    /// </summary>
    public void CancelCast()
    {
        OnCancellingCast(CreateEventArgs());
        CurrentAbility = null;
    }

    /// <summary>
    ///     Triggers the current ability's cooldown, invokes the ability, then nulls the ability
    /// </summary>
    public void FinishCast()
    {
        // TODO: Factor Haste into cooldown
        Owner.Cooldowns[CurrentAbility.Name] = Time.time + Power.GetHastedCD(CurrentAbility.Cooldown, Power.BaseHaste);

        CurrentAbility.Invoke(Owner, Target);

        OnFinishingCast(CreateEventArgs());
        CurrentAbility = null;
    }

    #region EventHandling
    public event EventHandler<CastEventArgs> StartingCast;
    public event EventHandler<CastEventArgs> CancellingCast;
    public event EventHandler<CastEventArgs> FinishingCast;

    protected CastEventArgs CreateEventArgs()
    {
        return new CastEventArgs
        {
            Mgr = Owner.Mgr,
            Owner = Owner,
            Ability = CurrentAbility
        };
    }

    protected virtual void OnStartingCast(CastEventArgs e)
    {
        var handler = StartingCast;

        if(handler != null)
        {
            StartingCast(this, e);
        }
    }

    protected virtual void OnCancellingCast(CastEventArgs e)
    {
        var handler = StartingCast;

        if (handler != null)
        {
            CancellingCast(this, e);
        }
    }

    protected virtual void OnFinishingCast(CastEventArgs e)
    {
        var handler = StartingCast;

        if (handler != null)
        {
            FinishingCast(this, e);
        }
    }

    #endregion
}
