using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Raider
{
    public Raider HoverTarget;
    public Raider SelectTarget;

    // Remove when implementing action bar/action slot
    public Ability Heal;
    public Ability SplashHeal;

    protected int AbilityPress;

    public Player(BattleManager mgr, PlayerInfo info)
        : base(mgr)
    {
        Name = info.Name;
        ItemLevel = info.Gear.AverageItemLevel;
        Role = Role.Healer;

        MaxHealth = Health = Power.BaseHP + info.Gear.TotalPlusHP;
        AbilityPower = Power.BaseAP + info.Gear.TotalAbilityPower;
        GlobalCooldown = Power.GetHastedGCD(info.Gear.TotalHaste);
        GCDFinish = 0;

        Debug.Log(string.Format("Haste: {0}; GCD: {1}", info.Gear.TotalHaste, GlobalCooldown));

        Heal = new Heal(this);
        SplashHeal = new SplashHeal(this);
    }

    public override void Tick()
    {
        base.Tick();

        // Get Escape input
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
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

        // Clear target
        if(Input.GetMouseButtonDown(1))
        {
            Mgr.UFManager.UnselectAll();
        }

        // Replace this with ActionSlot callback
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            AbilityPressed(Heal);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            AbilityPressed(SplashHeal);
        }
    }

    protected override void TickAbilities()
    {
        Heal.Tick();
        SplashHeal.Tick();
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
