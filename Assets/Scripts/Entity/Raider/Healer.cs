using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    protected OldAbility Heal;
    protected OldAbility SplashHeal;

    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Healer;

        Heal = new Heal(this);
        SplashHeal = new SplashHeal(this);
    }

    protected override void TickAbilities()
    {
        Heal.Tick();
        SplashHeal.Tick();
    }

    protected override void DoAbility()
    {
        var target = GetAction();

        if(target != null)
        {
            StartCast(target);
        }
    }

    /// <summary>
    /// Sets the current ability and returns the apropriate target. Returns null if no healing is needed.
    /// </summary>
    /// <returns></returns>
    protected Entity GetAction()
    {
        var raid = Mgr.Raid;
        var lowestHealth = raid.GetLowestHealth();

        if (lowestHealth == null) return null;

        var splash = raid.GetSplash(lowestHealth);

        int numHurt = raid.GetNumberHurt(splash);

        if(numHurt >= 6 && SplashHeal.OffCooldown)
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
