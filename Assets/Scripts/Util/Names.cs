using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Names
{
    public static List<string> List = new List<string>
    {
        "Bill",
        "Ted",
        "Stuart",
        "Robert",
        "William",
        "Philliam",
        "Mark",
        "Zucc",
        "Jeff",
        "Friend",
        "BnB",
        "Absolution",
        "Profanity",
        "EdBoy",
        "GothGF",
        "Wuce",
        "Nah",
        "Habibi",
        "Koda",
        "Kanye",
        "Boat",
        "Tj",
        "Leon",
        "Shoenice",
        "Gregory",
        "Barry",
        "Larry",
        "Garry",
        "Jay",
        "Poppy",
        "Bitman",
        "Joel",
        "MCRide",
        "Bonestorm",
        "Heron",
        "Birb"
    };

    public static string GetRandom()
    {
        return List[Random.Range(0, List.Count)];
    }
}
