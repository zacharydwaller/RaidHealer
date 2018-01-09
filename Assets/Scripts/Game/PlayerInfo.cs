using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string Name;

    public Gear Gear;

    public List<Ability> AbilityList;

    public PlayerInfo(string newName)
    {
        Name = newName;
        Gear = new Gear();

        // Have to set owner later
        AbilityList = new List<Ability>
        {
            new Heal(),
            new SplashHeal()
        };
    }
}
