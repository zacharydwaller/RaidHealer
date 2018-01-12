using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultice : AuraAbility
{
    public Poultice(Entity owner = null)
        : base(owner)
    {
        Name = "Poultice";
        CastAdd = 0f;
        Cooldown = 0f;
        ImagePath = "Image/Witch/poultice";
    }

    public override Aura CreateAura()
    {
        return new PoulticeBuff(Target as Raider, Owner);
    }
}
