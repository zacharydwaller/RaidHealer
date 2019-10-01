using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    public string Name { get; protected set; }
    public string ImagePath { get; protected set; }

    public TargetType TargetType { get; protected set; }
    public List<IAbilityEffect> Effects { get; protected set; }

    public int ManaCost { get; protected set; }
    public float CastTime { get; protected set; }
    public float Cooldown { get; protected set; }

    public void Invoke(Entity owner, Entity target)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(owner, this, target);
        }
    }
}
