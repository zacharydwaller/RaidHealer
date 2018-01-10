using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore : HealAbility
{
    public Restore(Entity owner = null)
        : base(owner)
    {
        Name = "Restore";
        CastAdd = 1.0f;
        PowerCoefficient = 1.25f;
        ImagePath = "Image/Cleric/restore";
    }
}
