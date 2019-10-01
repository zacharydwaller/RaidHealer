using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttuneBuff : Aura
{
    public float PowerMult = 0.4f;

    public AttuneBuff(Raider owner, Entity applier = null)
        : base(owner, applier)
    {
        Name = "Attune";
    }

    public override void Start()
    {
        foreach(Raider raider in Owner.Mgr.Raid.Raiders)
        {
            foreach(var aura in raider.Auras)
            {
                if(aura.GetType() == typeof(AttuneBuff))
                {
                    aura.Finish();
                    break;
                }
            }
        }

        base.Start();
    }

    // Override tick so it doesn't call base.Tick() and never expires
    public override void Tick()
    {
        //base.Tick();
    }
}