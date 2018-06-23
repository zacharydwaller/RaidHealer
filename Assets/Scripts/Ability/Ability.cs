using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public Entity Owner { get; protected set; }

    public string Name { get; protected set; }
    public string ImagePath { get; protected set; }

    public AbilityType AbilityType;
    public TargetType TargetType;

    public int NumTargets { get; private set; }

    public float PowerCoefficient { get; protected set; }
    public float TotalPower { get { return Owner.AbilityPower * PowerCoefficient; } }

    public bool IsCastedAbility { get; protected set; }
    public float CastAdd { get; protected set; }
    public float CastTime { get { return IsCastedAbility ? Owner.GlobalCooldown + CastAdd : 0; } }

    public float Cooldown { get; protected set; }

    public Ability(Entity owner = null)
    {
        Owner = owner;
    }

    public virtual void Invoke(IEnumerable<Entity> targets) { }
}
