using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HoT : Aura
{
    protected const float TickDelay = 3.0f;
    protected float NextTick;

    protected float Ticks { get { return Duration / TickDelay; } }

    public HoT(Raider owner, Entity applier)
        : base(owner, applier)
    {
        NextTick = Time.time + TickDelay;
    }

    public override void Tick()
    {
        DurationRemaining -= Time.deltaTime;

        if(Time.time >= NextTick)
        {
            NextTick += TickDelay;
            Owner.TakeHeal(Applier.AbilityPower * PowerCoefficient / Ticks);
        }

        if(DurationRemaining <= 0)
        {
            Finish();
        }
    }
}
