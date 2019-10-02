using System.Collections.Generic;

/// <summary>
///     Raidwide Damage. 30 second CD
/// </summary>
public class RainFire : Ability
{
    public RainFire()
    {
        Name = "Rain Fire";
        CastTime = 2.0f;
        Cooldown = 30.0f;

        Effects = new List<IAbilityEffect>()
        {
            new DamageEffectRaidwide(0.2f),
            new ApplyAuraEffectRaidwide(new RainFireDot())
        };
    }
}

public class RainFireDot : DotEffect
{
    public RainFireDot()
        : base()
    {
        Name = "Rain Fire";
        ImagePath = "";
        PowerCoefficient = 0.8f;
        Duration = 6.0f;
        TickDelay = 1.5f;
    }
}
