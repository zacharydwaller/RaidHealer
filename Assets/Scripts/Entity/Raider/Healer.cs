using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Raider
{
    public float AbilityUseDelay;
    public float AbilityUseTime = 0;

    public Healer(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Healer;

        AbilityUseDelay = GlobalCooldown * 2.0f;

        Abilities = new List<Ability>()
        {
            new Heal()
        };
    }

    protected override void SelectAbility()
    {
        if (Time.time >= AbilityUseTime)
        {
            QueuedAbility = Abilities[0]; // Heal
            AbilityUseTime = Time.time + AbilityUseDelay;
        }
    }

    protected override void DoAbility()
    {
        if (QueuedAbility == null) return;

        var target = GetTarget();

        if(target != null)
        {
            CastManager.StartCast(QueuedAbility, target);
            QueuedAbility = null;
        }
    }

    /// <summary>
    /// Returns the apropriate target for healing. Returns null if no healing is needed.
    /// </summary>
    /// <returns></returns>
    protected Entity GetTarget()
    {
        var targetCount = 3;
        var lowestHealths = Mgr.Raid.GetSmartAoE(targetCount);

        var index = (int) Random.Range(0, 3);

        if(lowestHealths[index].HealthPercent == 100)
        {
            return null;
        }
        else
        {
            return lowestHealths[index];
        }


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
