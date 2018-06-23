using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : OldAbility
{
    public HealAbility(Entity owner = null)
        : base(owner)
    {

    }

    public override void StartCast(Entity target = null)
    {
        base.StartCast(target);

        if(IsCastedAbility)
        {
            AddHealPredict();
        }
    }

    public override void CancelCast()
    {
        if(IsCastedAbility)
        {
            RemoveHealPredict();
        }
        
        base.CancelCast();
    }

    protected override void Do()
    {
        foreach (var raider in TargetList)
        {
            raider.TakeHeal(TotalPower);
        }

        if(IsCastedAbility)
        {
            RemoveHealPredict();
        }
        
        base.Do();
    }
}
