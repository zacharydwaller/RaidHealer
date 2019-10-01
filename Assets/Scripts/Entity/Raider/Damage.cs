public class Damage : Raider
{
    public Damage(BattleManager mgr)
        : base(mgr)
    {
        Role = Role.Damage;
    }

    protected override void DoAbility()
    {
        if(Mgr.Boss.IsAlive)
        {
            CastManager.StartCast(AutoAttack, Mgr.Boss);
        }
    }
}
