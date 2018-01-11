using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aura
{
    public Raider Owner;
    public Entity Applier;

    public float Duration;
    protected float DurationRemaining;

    public Aura(Raider owner, Entity applier = null)
    {
        Applier = applier;
        Owner = owner;

        Start();
    }

    public virtual void Start()
    {
        DurationRemaining = Duration;
    }

    public virtual void Tick()
    {
        DurationRemaining -= Time.deltaTime;

        if(DurationRemaining <= 0)
        {
            Finish();
        }
    }


    public virtual void Finish()
    {
        Owner.Auras.Remove(this);
    }
}
