using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Healer;

        Abilities = new List<Ability>()
        {
            new Heal()
        };
    }

    protected override void SelectAbility()
    {
        QueuedAbility = Abilities[0]; // Heal
    }

    protected override void DoAbility()
    {
        var target = GetTarget();

        if(target != null)
        {
            CastManager.StartCast(QueuedAbility, target);
        }
    }

    /// <summary>
    /// Returns the apropriate target for healing. Returns null if no healing is needed.
    /// </summary>
    /// <returns></returns>
    protected Entity GetTarget()
    {
        var raid = Mgr.Raid;
        var lowestHealth = raid.GetLowestHealth();

        return lowestHealth;

        // Uncomment for splash/chain heal support for AI healers
        //if (lowestHealth == null) return null;

        //var splash = raid.GetSplash(lowestHealth);
        //int numHurt = raid.GetNumberHurt(splash);

        //if(numHurt >= 6 && SplashHeal.OffCooldown)
        //{
        //    CurrentAbility = SplashHeal;
        //}
        //// else if numHurt >= 3 use ChainHeal
        //else if(numHurt >= 1)
        //{
        //    CurrentAbility = Heal;
        //}
        //else
        //{
        //    return null;
        //}

        //return lowestHealth;
    }
}
