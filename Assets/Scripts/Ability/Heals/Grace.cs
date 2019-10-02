using System.Collections.Generic;
/// <summary>
///     Standard flash heal
/// </summary>
public class Grace : Ability
{
    public Grace()
    {
        Name = "Grace";
        ImagePath = "Image/Cleric/grace";

        TargetType = TargetType.Friend;

        Effects = new List<IAbilityEffect>()
        {
            new HealEffect(1.0f)
        };

        ManaCost = 1000;
        CastTime = 1.5f;
    }
}
