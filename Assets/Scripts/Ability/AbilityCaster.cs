using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster
{
    public Ability Ability { get; private set; }
    public Entity Owner { get; private set; }
    public IList<Entity> TargetList { get; private set; }

    /// <summary>
    /// The Time that ability was fully casted last
    /// </summary>
    public float LastCast { get; private set; }
    public bool AbilityOffCooldown { get { return Time.time >= LastCast + Ability.Cooldown; } }

    private float TotalPower { get { return Owner.AbilityPower * Ability.PowerCoefficient; } }

    public AbilityCaster(Ability ability, Entity owner)
    {
        Ability = ability;
        Owner = owner;
        LastCast = Mathf.NegativeInfinity;
    }

    /// <summary>
    /// Starts casting the ability. Handles heal prediction, returns a CastTracker
    /// </summary>
    /// <returns></returns>
    public CastTracker StartCasting(Entity primaryTarget)
    {
        if(AbilityOffCooldown)
        {
            ConstructTargetList(primaryTarget);
            // add heal predict
            return new CastTracker(this, Ability.CastTime);
        }
        return null;
    }

    /// <summary>
    /// Should only be called by CastTracker. Cancels casting the ability. Removes heal prediction.
    /// </summary>
    public void CancelCasting()
    {
        // remove heal predict
    }

    /// <summary>
    /// Should only be called by CastTracker. Invokes the ability, removes heal prediction.
    /// </summary>
    public void FinishCasting()
    {
        // remove heal predict
        // invoke ability
        LastCast = Time.time;
    }

    private void ConstructTargetList(Entity primaryTarget)
    {
        var raid = Owner.Mgr.Raid;

        switch (Ability.TargetType)
        {
            case TargetType.Single:
                TargetList = new List<Entity> { primaryTarget };
                break;
            case TargetType.Chain:
                TargetList = raid.GetChain(primaryTarget, Ability.NumTargets);
                break;
        }
    }

    /// <summary>
    /// Adds heal predict to each target.
    /// Call when starting to cast heal.
    /// </summary>
    private void AddHealPredict()
    {
        foreach (var target in TargetList)
        {
            target.HealPredict += TotalPower;
        }
    }

    /// <summary>
    /// Removes heal predict from each target.
    /// Call when finishing or canceling a heal cast.
    /// </summary>
    private void RemoveHealPredict()
    {
        foreach (var target in TargetList)
        {
            target.HealPredict -= TotalPower;
        }
    }
}
