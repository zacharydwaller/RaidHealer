public abstract class AuraEffect
{
    public string Name { get; protected set; }
    public string ImagePath { get; protected set; }
    public float PowerCoefficient { get; protected set; }
    public float Duration { get; protected set; }
    public bool Friendly { get; protected set; } = true;

    // TODO: Take Haste into account
    public float TickDelay { get; protected set; } = 3.0f;
    public float Ticks { get { return (Duration / TickDelay); } }

    public abstract void Invoke(Entity parent, Entity owner, Aura aura);
}
