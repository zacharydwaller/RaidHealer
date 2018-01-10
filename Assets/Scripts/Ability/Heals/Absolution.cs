using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absolution : HealAbility
{
    public Absolution(Entity owner = null)
        : base(owner)
    {
        Name = "Absolution";
        CastAdd = 0f;
        PowerCoefficient = 2f;
        Cooldown = 15f;
        ImagePath = "Image/Cleric/absolution";
    }
}
