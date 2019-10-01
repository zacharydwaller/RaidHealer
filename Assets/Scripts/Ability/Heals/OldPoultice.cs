using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Will do a small amount of healing, then apply hot
/// </summary>
public class OldPoultice : AuraAbility
{
    public OldPoultice(Entity owner = null)
        : base(owner)
    {
        Name = "Poultice";
        CastAdd = 0f;
        Cooldown = 0f;
        ImagePath = "Image/Witch/poultice";
        PowerCoefficient = 0.25f;
    }

    public override Aura CreateAura()
    {
        return new PoulticeBuff(Target as Raider, Owner);
    }
}
