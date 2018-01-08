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

    public bool Ready { get { return CooldownRemaining <= 0; } }

    public Entity Owner;

    public Ability(Entity owner)
    {
        Owner = owner;
    }

    public void Tick()
    {
        CooldownRemaining = Mathf.Max(CooldownRemaining - Time.deltaTime, 0.0f);
    }

    public virtual void StartCast(Entity target, float power) { }

    public virtual void CancelCast(Entity target, float power) { }

    public virtual void Do(Entity target, float power)
    {
        Owner.Mgr.LogAction(Owner, target, this);   
    }

    public void AddHealPredict(Entity target, float power)
    {
        target.HealPredict += Owner.AbilityPower * PowerCoefficient;
    }

    public void RemoveHealPredict(Entity target, float power)
    {
        target.HealPredict -= Owner.AbilityPower * PowerCoefficient;
    }
}
