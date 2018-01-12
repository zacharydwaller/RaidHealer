using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraAbility : Ability
{
    public AuraAbility(Entity owner = null)
        : base(owner)
    {

    }

    protected override void Do()
    {
        var targetRaider = Target as Raider;
        targetRaider.AddAura(CreateAura());

        base.Do();
    }

    public virtual Aura CreateAura()
    {
        return null;
    }
}
