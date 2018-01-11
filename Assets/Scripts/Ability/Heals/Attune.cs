using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attune : Ability
{
    public Attune(Entity owner = null)
        : base(owner)
    {
        Name = "Attunement";
        CastAdd = 0f;
        Cooldown = 4.0f;
        ImagePath = "Image/Cleric/attune";
    }

    protected override void Do()
    {
        var targetRaider = Target as Raider;
        targetRaider.AddAura(new AttuneBuff(targetRaider));

        base.Do();
    }
}
