using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulticeBuff : HoT
{
    public PoulticeBuff(Raider owner, Entity applier)
        : base(owner, applier)
    {
        Name = "Poultice";
        Duration = 18f;
        PowerCoefficient = 2.0f;

        Start();
    }
}
