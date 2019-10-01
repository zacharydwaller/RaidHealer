using UnityEngine;

[System.Serializable]
public class Raider : Entity
{
    public Role Role;

    public float ItemLevel;

    protected const float powerStd = 10;
    protected float gcdStd = 0.15f;

    public Raider(BattleManager mgr)
        :base(mgr)
    {
        Name = Names.GetRandom();
        ItemLevel = Distribution.GetRandom(Mgr.RaidItemLevel, powerStd);

        MaxHealth = Health = Power.ScaleValue(Power.BaseHP, ItemLevel);
        AbilityPower = Power.ScaleValue(Power.BaseAP, ItemLevel);

        float haste = Mathf.Abs(Power.BaseGCD - Distribution.GetRandom(Power.BaseGCD, gcdStd));
        GlobalCooldown = Mathf.Max(GlobalCooldown - haste, Power.MinGCD);
    }

    public override void Tick()
    {
        base.Tick();

        for(int i = 0; i < Auras.Count; i++)
        {
            Auras[i].Tick();
        }

        // Ready for new cast
        if (CastManager.ReadyToCast && this != Mgr.Player)
        {
            SelectAbility();
            DoAbility();
        }
    }
}
