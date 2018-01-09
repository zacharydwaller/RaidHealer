using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raider
{
    public Raider HoverTarget;
    public Raider SelectTarget;

    // Remove when implementing action bar/action slot
    public Ability Heal;

    protected int AbilityPress;

    public Player(BattleManager mgr)
        : base(mgr)
    {
        Name = "Player";
        Role = Role.Healer;
        GlobalCooldown = baseGCD;

        GCDFinish = 0;

        Heal = new Heal(this);
    }

    public override void Tick()
    {
        base.Tick();

        // Get Escape input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsCasting)
            {
                CancelCasting();
            }
            else
            {
                // Pause
            }
        }

        // Replace this with ActionSlot callback
        if(Input.GetKey(KeyCode.Alpha3))
        {
            AbilityPressed(Heal);
        }
    }

    public void AbilityPressed(Ability ability)
    {
        if(!IsCasting && GCDReady)
        {
            CurrentAbility = ability;

            Entity target;

            if(HoverTarget != null)
            {
                target = HoverTarget;
            }
            else if(SelectTarget != null)
            {
                target = SelectTarget;
            }
            else
            {
                target = this;
            }

            StartCasting(target);
        }
    }
}
