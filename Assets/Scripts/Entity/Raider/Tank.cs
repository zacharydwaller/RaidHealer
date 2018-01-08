using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Raider
{
    public Tank(BattleManager mgr)
        : base(mgr)
    {
        Name = "Tank";
        Role = Role.Tank;
        MaxHealth = Health *= 4.0f;
        AbilityPower *= 0.75f;
    }
}
