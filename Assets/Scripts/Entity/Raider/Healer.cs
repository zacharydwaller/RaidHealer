using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Healer;
        CurrentAbility = new Heal(this);
    }

    public override void Tick()
    {
        if (GCDReady && !IsCasting)
        {
            var lowestHealth = Mgr.Raid.GetLowestHealth();

            if(lowestHealth != null)
            {
                GCDFinish += GlobalCooldown;
                StartCasting(lowestHealth);
                CastTarget.HealPredict += AbilityPower * CurrentAbility.PowerCoefficient;
            }
            return;
        }

        if (IsCasting)
        {
            CastRemaining -= Time.deltaTime;

            if(CastTarget == null || CastTarget.IsDead)
            {
                CastTarget.HealPredict -= AbilityPower * CurrentAbility.PowerCoefficient;
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
            CastTarget.HealPredict -= AbilityPower * CurrentAbility.PowerCoefficient;
            CurrentAbility.Do(CastTarget, AbilityPower);
        }

        IsCasting = false;
    }
}
