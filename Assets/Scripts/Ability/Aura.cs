using UnityEngine;

public class Aura
{
    public Entity Parent { get; protected set; }
    public Entity Owner { get; protected set; }

    public AuraEffect AuraEffect { get; protected set; }

    public string Name { get => AuraEffect.Name; }
    public float Duration { get => AuraEffect.Duration; }

    public float ExpirationTime { get; protected set; }
    public float DurationRemaining { get => ExpirationTime - Time.time; }
    public float NextTick { get; protected set; }

    public Aura(Entity parent, Entity owner, AuraEffect auraEffect)
    {
        Parent = parent;
        Owner = owner;
        AuraEffect = auraEffect;

        ExpirationTime = Time.time + Duration;
        NextTick = Time.time + AuraEffect.TickDelay;
    }

    public virtual void Tick()
    {
        if(Time.time >= NextTick)
        {
            AuraEffect.Invoke(Parent, Owner, this);
            NextTick += AuraEffect.TickDelay;
        }

        if(DurationRemaining <= 0)
        {
            Finish();
        }
    }

    public virtual void Finish()
    {
        //Owner.Auras.Remove(this);
    }
}
