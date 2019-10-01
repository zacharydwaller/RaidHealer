using System.Collections.Generic;

/// <summary>
///     Does a small amount of healing and adds a hot
/// </summary>
public class Poultice : Ability
{
    public Poultice()
    {
        Name = "Poultice";
        ImagePath = "Image/Witch/poultice";

        TargetType = TargetType.Friend;

        var healCoeff = 0.5f;
        var hotCoeff = 2.0f;

        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(healCoeff),
            // new ApplyAura(new HotEffect(hotCoeff))
        };

        ManaCost = 1000;
        CastTime = 0.0f;
        Cooldown = 0.0f;
    }
}
