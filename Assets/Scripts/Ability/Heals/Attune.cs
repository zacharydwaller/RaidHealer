using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attune : AuraAbility
{
    public Attune(Entity owner = null)
        : base(owner)
    {
        Name = "Attune";
        CastAdd = 0f;
        Cooldown = 4.0f;
        ImagePath = "Image/Cleric/attune";
    }

    public override Aura CreateAura()
    {
        return new AttuneBuff(Target as Raider);
    }
}
