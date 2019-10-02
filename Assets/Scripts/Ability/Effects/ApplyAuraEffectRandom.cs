using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAuraEffectRandom : IAbilityEffect
{
    public AuraEffect AuraEffect { get; protected set; }

    public ApplyAuraEffectRandom(AuraEffect auraEffect)
    {
        AuraEffect = auraEffect;
    }

    public void Invoke(Entity owner, Ability parent, Entity _)
    {
        var target = owner.Mgr.Raid.GetRandom();
        var aura = new Aura(owner, target, AuraEffect);
        target.AddAura(aura);
    }
}
