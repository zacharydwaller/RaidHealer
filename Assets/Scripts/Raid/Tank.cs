﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Raider
{
    public Tank()
    {
        Name = "Tank";
        Role = Role.Tank;
        MaxHealth = Health *= 2.0f;
        AbilityPower *= 0.5f;
    }
}
