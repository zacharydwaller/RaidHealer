using System.Collections.Generic;

public class DamageEffectRandomSplash : IAbilityEffect
{
    public float PowerCoefficient { get; protected set; }

    public DamageEffectRandomSplash(float powerCoeff)
    {
        PowerCoefficient = powerCoeff;
    }

    public void Invoke(Entity owner, Ability parent, Entity _)
    {
        var raid = owner.Mgr.Raid;
        
        var center = raid.GetRandom();
        center.TakeDamage(owner.AbilityPower * PowerCoefficient / 2.0f);

        var splashTargets = (List<Entity>)raid.GetSplash(center);
        foreach (var target in splashTargets)
        {
            target.TakeDamage(owner.AbilityPower * PowerCoefficient);
        }
    }
}
