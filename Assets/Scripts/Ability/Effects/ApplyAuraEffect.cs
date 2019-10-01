public class ApplyAuraEffect : IAbilityEffect
{
    public AuraEffect AuraEffect { get; protected set; }

    public ApplyAuraEffect(AuraEffect auraEffect)
    {
        AuraEffect = auraEffect;
    }

    public void Invoke(Entity owner, Ability parent, Entity target)
    {
        var aura = new Aura(owner, target, AuraEffect);
        target.AddAura(aura);
    }
}
