public class DamageEffect : IAbilityEffect
{
    public float PowerCoefficient { get; protected set; }

    public DamageEffect(float powerCoeff)
    {
        PowerCoefficient = powerCoeff;
    }

    public void Invoke(Entity owner, Ability parent, Entity target)
    {
        target.TakeDamage(owner.AbilityPower * PowerCoefficient);
    }
}
