using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string Name;

    public Gear Gear;

    public List<Ability> AbilityList;

    public PlayerInfo(string newName, float itemLevel)
    {
        Name = newName;
        Gear = new Gear(itemLevel);

        // Have to set owner later
        //AbilityList = new List<OldAbility>
        //{
        //    new Restore(),
        //    new Grace(),
        //    new Absolution(),
        //    new Prayer(),
        //    new HallowGround(),
        //    new Supplication(),
        //    //new Attune()
        //    //new OldPoultice(),
        //};

        AbilityList = new List<Ability>()
        {
            //new Poultice()
            new Grace(),
            new Absolution()
        };
    }
}
