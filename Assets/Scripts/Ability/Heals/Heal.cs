using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public Heal()
    {
        Name = "Heal";
        ImagePath = "";

        TargetType = TargetType.Friend;
        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(1.0f)
        };

        ManaCost = 1000;
        CastTime = 2.0f;
    }
}
