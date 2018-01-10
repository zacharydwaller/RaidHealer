using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore : HealAbility
{
    public Restore(Entity owner = null)
        : base(owner)
    {
        Name = "Restore";
        CastAdd = 0.25f;
        PowerCoefficient = 1.0f;
        ImagePath = "Image/Cleric/restore";
    }

    protected override void Do()
    {
        Target.TakeHeal(TotalPower);

        base.Do();
    }
}
