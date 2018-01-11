using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttuneBuff : Aura, ICastEventHandler
{
    public float PowerMult = 0.4f;

    public AttuneBuff(Raider owner, Entity applier = null)
        : base(owner, applier)
    {
        var player = Owner.Mgr.Player;
        player.StartingCast += OnStartingCast;
        player.CancellingCast += OnCancellingCast;
        player.FinishingCast += OnFinishingCast;
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

    public void OnStartingCast(object sender, CastEventArgs e)
    {
        Owner.HealPredict += e.Ability.TotalPower * PowerMult;
    }

    public void OnCancellingCast(object sender, CastEventArgs e)
    {
        Owner.HealPredict -= e.Ability.TotalPower * PowerMult;
    }

    public void OnFinishingCast(object sender, CastEventArgs e)
    {
        Owner.TakeHeal(e.Ability.TotalPower * PowerMult);
        Owner.HealPredict -= e.Ability.TotalPower * PowerMult;
    }
}