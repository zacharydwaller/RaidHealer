using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity
{
    public int ID;

    public string Name;

    public float MaxHealth;
    public float Health;

    public float HealthPercent { get { return Health / MaxHealth * 100.0f;  } }

    public float HealPredict;

    public float HealthPercentPredict { get { return (Health + HealPredict) / MaxHealth * 100.0f; } }

    public float GlobalCooldown = 1.5f;
    protected float GCDFinish;

    public float AbilityPower;

    [SerializeField]
    public Ability CurrentAbility;

    public bool IsCasting { get { return CurrentAbility != null ? CurrentAbility.IsBeingCasted : false; } }

    [HideInInspector]
    public BattleManager Mgr;

    protected static int currentID;

    public bool GCDReady { get { return !IsDead && (Time.time >= GCDFinish); } }
    

    public bool IsAlive { get { return Health > 0; } }
    public bool IsDead { get { return !IsAlive; } }

    public float GCDProgress { get { return GCDReady ? 1.0f : 1.0f - (Mathf.Max(GCDFinish - Time.time, 0) / GlobalCooldown); } }

    public Entity(BattleManager mgr)
    {
        Mgr = mgr;
        Health = MaxHealth;
        GCDFinish = GlobalCooldown;

        currentID++;
        ID = currentID;
    }

    /// <summary>
    /// Base Entity Tick. Override to include more behavior. Always call base.Tick() at beginning of overriden function.
    /// </summary>
    public virtual void Tick()
    {
        TickAbilities();
    }

    /// <summary>
    /// Should call Ability.Tick() on all abilities.
    /// Override to include which abilities are being ticked.
    /// </summary>
    protected virtual void TickAbilities() { }

    public void SetManager(BattleManager mgr)
    {
        Mgr = mgr;
    }

    protected virtual void SelectAbility() { }
    protected virtual void DoAbility() { }

    /// <summary>
    /// Triggers the GCD and starts CurrentAbility's cast if ability is not on cooldown
    /// </summary>
    /// <param name="target"></param>
    public void StartCasting(Entity target)
    {
        if(CurrentAbility.OffCooldown)
        {
            GCDFinish = Time.time + GlobalCooldown;
            CurrentAbility.StartCast(target);
        }
    }

    /// <summary>
    /// Cancels CurrentAbility
    /// </summary>
    public void CancelCasting()
    {
        CurrentAbility.CancelCast();
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
