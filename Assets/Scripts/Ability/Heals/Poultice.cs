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

        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(0.5f),
            new ApplyAuraEffect(new PoulticeHot())
        };

        ManaCost = 1000;
        CastTime = 0.0f;
        Cooldown = 0.0f;
    }
}

public class PoulticeHot : HotEffect
{
    public PoulticeHot()
        : base()
    {
        Name = "Poultice";
        PowerCoefficient = 3.0f;
        Duration = 18.0f;
    }
}