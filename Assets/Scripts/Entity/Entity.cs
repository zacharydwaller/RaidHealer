using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity
{
    public int Id;
    protected static int LastIdUsed;

    public string Name;

    public float MaxHealth;
    public float Health;

    public bool IsAlive { get { return Health > 0; } }
    public bool IsDead { get { return !IsAlive; } }
    public float HealthPercent { get { return Health / MaxHealth * 100.0f;  } }

    public float HealPredict;
    public float HealthPercentPredict { get { return (Health + HealPredict) / MaxHealth * 100.0f; } }

    public float GlobalCooldown = 1.5f;
    public float AbilityPower;

    public List<Aura> Auras;
    public List<Ability> Abilities;

    /// <summary>
    ///  Key = Spell Name; Value = When spell is ready
    /// </summary>
    public Dictionary<string, float> Cooldowns { get; protected set; }

    protected Ability QueuedAbility;
    protected Ability AutoAttack;

    public CastManager CastManager { get; protected set; }

    [HideInInspector]
    public BattleManager Mgr;

    public Entity(BattleManager mgr)
    {
        Mgr = mgr;
        Health = MaxHealth;

        Auras = new List<Aura>();
        Abilities = new List<Ability>();
        Cooldowns = new Dictionary<string, float>();
        AutoAttack = new AutoAttack();

        CastManager = new CastManager(this);

        Id = ++LastIdUsed;
    }

    public virtual void Tick()
    {
        CastManager.Tick();

        Auras.ForEach(a => a.Tick());
        Auras.RemoveAll(a => a.DurationRemaining <= 0);
    }

    /// <summary>
    ///     Override for AI entities to select the QueuedAbility
    ///     Should have no override for Player
    /// </summary>
    protected virtual void SelectAbility() { }

    /// <summary>
    ///     Override for AI entities to select a target and start casting the QueuedAbility
    ///     Override for Player to start casting the QueuedAbility on current target
    /// </summary>
    protected virtual void DoAbility() { }

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

    public void AddAura(Aura newAura)
    {
        // If aura already exists, remove old one
        var oldAura = Auras.RemoveAll(a => a.Name.Equals(newAura.Name) && a.Owner.Id == newAura.Owner.Id);

        Auras.Add(newAura);
    }
}
