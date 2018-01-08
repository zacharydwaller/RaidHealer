using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    protected Ability Heal;
    protected Ability SplashHeal;

    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Healer;

        Heal = new Heal(this);
        SplashHeal = new SplashHeal(this);
    }

    public override void Tick()
    {
        if (GCDReady && !IsCasting)
        {
            var target = GetAction();

            if(target != null)
            {
                GCDFinish += GlobalCooldown;
                StartCasting(target);
                CastTarget.HealPredict += AbilityPower * CurrentAbility.PowerCoefficient;
            }
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

    /// <summary>
    /// Sets the current ability and returns the apropriate target. Returns null if no healing is needed.
    /// </summary>
    /// <returns></returns>
    protected Raider GetAction()
    {
        var raid = Mgr.Raid;
        var lowestHealth = raid.GetLowestHealth();

        if (lowestHealth == null) return null;

        var splash = raid.GetSplash(lowestHealth);

        int numHurt = raid.GetNumberHurt(splash);

        if(numHurt >= 6 && SplashHeal.Ready)
        {
            CurrentAbility = SplashHeal;
        }
        // else if numHurt >= 3 use ChainHeal
        else if(numHurt >= 1)
        {
            CurrentAbility = Heal;
        }
        else
        {
            return null;
        }

        return lowestHealth;
    }
}
