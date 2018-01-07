using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal()
    {
        Name = "Heal";
        CastTime = 2.0f;
        PowerCoefficient = 1.0f;
    }

    public override void Do(Entity target, float power)
    {
        target.TakeHeal(power * PowerCoefficient);
    }
}
