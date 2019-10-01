public interface IAbilityEffect
{
    float PowerCoefficient { get; }

    void Invoke(Entity owner, Ability parent, Entity target);
}
