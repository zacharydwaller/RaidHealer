public class HealEffect : IAbilityEffect
{
    public float PowerCoefficient { get; protected set; }

    public HealEffect(float powerCoeff)
    {
        PowerCoefficient = powerCoeff;
    }

    public void Invoke(Entity owner, Ability parent, Entity target)
    {
        target.TakeHeal(owner.AbilityPower * PowerCoefficient);
    }
}
