using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : HealAbility
{
    public Heal(Entity owner = null)
        : base(owner)
    {
        Name = "Heal";
        CastAdd = 0.25f;
        PowerCoefficient = 0.25f;
    }
}
