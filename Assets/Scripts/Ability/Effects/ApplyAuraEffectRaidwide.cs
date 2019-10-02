public class ApplyAuraEffectRaidwide : IAbilityEffect
{
    public AuraEffect AuraEffect { get; protected set; }

    public ApplyAuraEffectRaidwide(AuraEffect auraEffect)
    {
        AuraEffect = auraEffect;
    }

    public void Invoke(Entity owner, Ability parent, Entity _)
    {
        var raidTargets = owner.Mgr.Raid.GetAoE();

        foreach(var target in raidTargets)
        {
            var aura = new Aura(owner, target, AuraEffect);
            target.AddAura(aura);
        }
    }
}
