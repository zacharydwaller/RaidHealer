using System.Collections.Generic;

public class DamageEffectRaidwide : IAbilityEffect
{
    public float PowerCoefficient { get; protected set; }

    public DamageEffectRaidwide(float powerCoeff)
    {
        PowerCoefficient = powerCoeff;
    }

    public void Invoke(Entity owner, Ability parent, Entity _)
    {
        var raidTargets = (List<Entity>) owner.Mgr.Raid.GetAoE();
        foreach (var target in raidTargets)
        {
            target.TakeDamage(owner.AbilityPower * PowerCoefficient);
        }
    }
}
