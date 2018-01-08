using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public string Name;
    public float CastTime;
    public float PowerCoefficient;

    public float Cooldown;
    public float CooldownRemaining;

    public Entity Owner;

    public Ability(Entity owner)
    {
        Owner = owner;
    }

    public void Tick()
    {
        CooldownRemaining = Mathf.Max(CooldownRemaining - Time.deltaTime, 0.0f);
    }

    public virtual void Do(Entity target, float power)
    {
        Owner.Mgr.LogAction(Owner, target, this);   
    }
}
