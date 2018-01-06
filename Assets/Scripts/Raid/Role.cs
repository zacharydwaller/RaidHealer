using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Role
{
    Tank, Damage, Healer
}

public static class RoleUtil
{
    public static Color GetColor(Role role)
    {
        switch(role)
        {
            case Role.Tank: return Color.blue;
            case Role.Healer: return Color.green;
            case Role.Damage: return Color.red;
        }
        return Color.white;
    }
}