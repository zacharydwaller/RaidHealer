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
        CurrentAbility = new Heal();
    }

    public override void Tick()
    {
        if (GCDReady && !IsCasting)
        {
            GCDFinish += GlobalCooldown;
            StartCasting();
            return;
        }

        if (IsCasting)
        {
            CastRemaining -= Time.deltaTime;
        }

        if (CastReady)
        {
            DoAbility();
        }
    }

    public override void DoAbility()
    {
        GCDFinish += GlobalCooldown;

        var lowestHealth = Mgr.Raid.GetLowestHealth();

        CurrentAbility.Do(lowestHealth, AbilityPower);

        IsCasting = false;
    }
}
