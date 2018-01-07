using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity
{
    public string Name;

    public float MaxHealth;
    public float Health;

    public float HealthPercent { get { return Health / MaxHealth * 100.0f;  } }

    public float GlobalCooldown = 1.5f;
    protected float GCDFinish;

    [SerializeField]
    public Ability CurrentAbility;
    public bool IsCasting = false;
    public float CastRemaining;

    protected BattleManager Mgr;

    public bool GCDReady { get { return !IsDead && (Time.time >= GCDFinish); } }
    public bool IsAlive { get { return Health > 0; } }
    public bool IsDead { get { return !IsAlive; } }
    public bool CastReady { get { return IsCasting && (CastRemaining <= 0); } }

    public Entity(BattleManager mgr)
    {
        Mgr = mgr;
        Health = MaxHealth;
        GCDFinish = GlobalCooldown;
    }

    public virtual void Tick() { }

    public void SetManager(BattleManager mgr)
    {
        Mgr = mgr;
    }

    public void StartCasting()
    {
        CastRemaining = CurrentAbility.CastTime;
        IsCasting = true;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        Health -= amount;
        if (Health < 0) Health = 0.0f;
    }

    public void TakeHeal(float amount)
    {
        if (IsDead) return;

        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
}
