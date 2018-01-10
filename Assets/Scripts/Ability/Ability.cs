using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Ability
{
    public string Name;

    public string ImagePath;

    public float PowerCoefficient;
    public float TotalPower { get { return Owner.AbilityPower * PowerCoefficient; } }

    /// <summary>
    /// Total cast time is Owner's GCD + Cast Add
    /// e.g. For a 2 second casted spell, make CastAdd = 0.5f
    /// </summary>
    public float CastAdd;
    public float CastRemaining;

    /// <summary>
    /// Spell's total cast time with CastAdd and Owner's GCD factored in
    /// </summary>
    public float CastTime { get { return IsCastedAbility ? Owner.GlobalCooldown + CastAdd : 0; } }

    /// <summary>
    /// Gives a float 0-1 to indicate percentage of cast completion.
    /// Useful for progress bars.
    /// </summary>
    public float CastProgress { get { return 1.0f - (CastRemaining / CastTime); } }

    public bool IsCastedAbility { get { return CastAdd > 0; } }
    public bool IsBeingCasted { get { return CastRemaining > 0; } }
    public bool CastReady { get { return CastRemaining == 0; } }

    public float Cooldown;
    public float CooldownRemaining;

    /// <summary>
    /// Gives a float 0-1 to indicate percentage of cooldown completion.
    /// Useful for progress bars.
    /// </summary>
    public float CooldownProgress { get { return 1.0f - (CooldownRemaining / Cooldown); } }

    public bool OffCooldown { get { return CooldownRemaining <= 0f; } }

    public Entity Owner;

    public List<Entity> TargetList;
    public Entity Target { get { return TargetList.Count > 0 ? TargetList[0] : null; } }

    public Ability(Entity owner = null)
    {
        Owner = owner;
        TargetList = new List<Entity>();
    }

    /// <summary>
    /// Called every frame to lower cooldown remaining.
    /// Override for channeled spells and HoTs/DoTs to tick healing/damage.
    /// </summary>
    public virtual void Tick()
    {
        // Being casted, tick cast timer and check if ready or if target is dead
        if(IsBeingCasted)
        {
            CastRemaining = Mathf.Max(CastRemaining - Time.deltaTime, 0.0f);

            if (TargetList.All(t => t.IsDead))
            {
                CancelCast();
            }
            else if (CastReady)
            {
                Do();
            }
        }
        // Not being casted, tick cooldown
        else
        {
            CooldownRemaining = Mathf.Max(CooldownRemaining - Time.deltaTime, 0.0f);
        }
    }

    /// <summary>
    /// Default behavior for single target cast, adds target to Targets.
    /// Override for chain/splash/aoe abilities.
    /// </summary>
    /// <param name="target"></param>
    public virtual void StartCast(Entity target = null)
    {
        if (target != null)
        {
            TargetList.Add(target);
        }

        if (IsCastedAbility)
        {
            CastRemaining = CastTime;
        }
        else
        {
            Do();
        }
    }

    /// <summary>
    /// Clears Targets, doesn't trigger cooldown. Call base.CancelCast() last in overridden function.
    /// </summary>
    public virtual void CancelCast()
    {
        TargetList.Clear();
        CastRemaining = 0f;
    }

    /// <summary>
    /// Override to make ability do action. base.Do() should be called at end of overridden function.
    /// Default behavior logs action for each Target, clears Targets, and triggers its cooldown
    /// </summary>
    /// <param name="target"></param>
    protected virtual void Do()
    {
        if(TargetList.Count > 0)
        {
            foreach (var entity in TargetList)
            {
                Owner.Mgr.LogAction(Owner, entity, this);
            }

            TargetList.Clear();
        }

        CooldownRemaining = Cooldown;
    }

    /// <summary>
    /// Adds heal predict to each target.
    /// Call when starting to cast heal.
    /// </summary>
    public void AddHealPredict()
    {
        foreach(var target in TargetList)
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
        foreach (var target in TargetList)
        {
            target.HealPredict -= TotalPower;
        }
    }
}
