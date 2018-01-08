using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public string Name;
    public float CastTime;
    public float PowerCoefficient;

    public float TotalPower { get { return Owner.AbilityPower * PowerCoefficient; } }

    public float Cooldown;
    public float CooldownRemaining;

    public bool Ready { get { return CooldownRemaining <= 0; } }

    public Entity Owner;

    public List<Entity> Targets;

    public Ability(Entity owner)
    {
        Owner = owner;
        Targets = new List<Entity>();
    }

    /// <summary>
    /// Called every frame to lower cooldown remaining.
    /// Override for channeled spells and HoTs/DoTs to tick healing/damage.
    /// </summary>
    public void Tick()
    {
        CooldownRemaining = Mathf.Max(CooldownRemaining - Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// Default behavior for single target cast, adds target to Targets.
    /// Override for chain/splash/aoe abilities.
    /// </summary>
    /// <param name="target"></param>
    public virtual void StartCast(Entity target)
    {
        Targets.Clear();
        Targets.Add(target);
    }

    /// <summary>
    /// Clears Targets, doesn't trigger cooldown.
    /// </summary>
    public virtual void CancelCast()
    {
        Targets.Clear();
    }

    /// <summary>
    /// Do() for instant cast spells where Targets is not set.
    /// Override to make ability do action. base.Do() should be called at end of overridden function.
    /// Default behavior logs action for target and Targets and triggers the cooldown.
    /// </summary>
    /// <param name="target"></param>
    public virtual void Do(Entity target = null)
    {
        if(target != null)
        {
            Owner.Mgr.LogAction(Owner, target, this);
        }

        if(Targets.Count > 0)
        {
            foreach (var entity in Targets)
            {
                Owner.Mgr.LogAction(Owner, entity, this);
            }
        }
        CooldownRemaining = Cooldown;
    }

    /// <summary>
    /// Adds heal predict to each target.
    /// Call when starting to cast heal.
    /// </summary>
    public void AddHealPredict()
    {
        foreach(var target in Targets)
        {
            target.HealPredict += TotalPower;
        }
    }

    /// <summary>
    /// Removes heal predict from each target.
    /// Call when finishing or canceling a heal cast.
    /// </summary>
    public void RemoveHealPredict()
    {
        foreach (var target in Targets)
        {
            target.HealPredict -= TotalPower;
        }
    }
}
