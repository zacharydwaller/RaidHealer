using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Name = "Healer";
        Role = Role.Healer;
        CurrentAbility = new Heal(this);
    }

    public override void Tick()
    {
        if (GCDReady && !IsCasting)
        {
            GCDFinish += GlobalCooldown;
            StartCasting(Mgr.Raid.GetLowestHealth());
            return;
        }

        if (IsCasting)
        {
            CastRemaining -= Time.deltaTime;

            if(CastTarget == null || CastTarget.IsDead)
            {
                IsCasting = false;
            }
        }

        if (CastReady)
        {
            DoAbility();
        }
    }

    public override void DoAbility()
    {
        if(CastTarget != null && CastTarget.IsAlive)
        {
            CurrentAbility.Do(CastTarget, AbilityPower);
        }

        IsCasting = false;
    }
}
