using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTracker
{
    public float CastRemaining { get; private set; }
    public bool Finished { get; private set; }

    private AbilityCaster caster;

    public CastTracker(AbilityCaster caster, float duration)
    {
        this.caster = caster;

        if(duration > 0)
        {
            Finished = false;
            CastRemaining = duration;
        }
        else
        {
            Finished = true;
            caster.FinishCasting();
        }
    }

    public void Tick()
    {
        if(!Finished)
        {
            CastRemaining -= Time.deltaTime;

            if (CastRemaining <= 0)
            {
                Finished = true;
                caster.FinishCasting();
            }
        }
    }

    public void Cancel()
    {
        Finished = true;
        caster.CancelCasting();
    }
}
