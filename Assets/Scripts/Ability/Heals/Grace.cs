using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grace : HealAbility
{
    public Grace(Entity owner = null)
        : base(owner)
    {
        Name = "Grace";
        CastAdd = 0.01f;
        PowerCoefficient = 1.0f;
        ImagePath = "Image/Cleric/grace";
    }
}
