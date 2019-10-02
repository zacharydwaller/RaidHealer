using System.Collections.Generic;

public class Skewer : Ability
{
    public Skewer()
    {
        Name = "Skewer";
        CastTime = 1.5f;
        Cooldown = 10.0f;

        Effects = new List<IAbilityEffect>()
        {
            new ApplyAuraEffectRandom(new SkewerDot())
        };
    }
}

public class SkewerDot : DotEffect
{
    public SkewerDot()
        : base()
    {
        Name = "Skewer";
        ImagePath = "";
        PowerCoefficient = 1.0f;
        TickDelay = 1.5f;
        Duration = 9.0f;
        //Friendly = false;
    }
}