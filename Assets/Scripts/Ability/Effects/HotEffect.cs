public abstract class HotEffect : AuraEffect
{
    public override void Invoke(Entity parent, Entity owner, Aura aura)
    {
        owner.TakeHeal(parent.AbilityPower * PowerCoefficient / Ticks);
    }
}
