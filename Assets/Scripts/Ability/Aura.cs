using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aura
{
    public Entity Owner;
    public Entity Applier;

    public Aura(Entity owner, Entity applier = null)
    {
        Applier = applier;
        Owner = owner;
    }

    public virtual void Start() { }
    public virtual void Tick() { }
    public virtual void Finish() { }
}
