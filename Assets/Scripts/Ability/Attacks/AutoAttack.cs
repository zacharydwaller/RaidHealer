using System.Collections.Generic;

public class AutoAttack : Ability
{
    public AutoAttack()
    {
        Name = "Attack";
        ImagePath = "";

        TargetType = TargetType.Foe;
        Effects = new List<IAbilityEffect>()
        {
            new DamageEffect(1.0f)
        };
    }

}
