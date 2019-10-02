public class DotEffect : AuraEffect
{
    public override void Invoke(Entity parent, Entity owner, Aura aura)
    {
        owner.TakeDamage(parent.AbilityPower * PowerCoefficient / Ticks);
    }
}
