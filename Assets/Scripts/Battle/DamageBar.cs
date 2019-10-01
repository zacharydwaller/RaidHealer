using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageBar : MonoBehaviour
{
    public int ID;

    public Text NameText;
    public Text DamageText;
    public Slider Bar;

    protected float TargetBarValue;
    protected float LerpConstant = 0.3f;

    // DamageDone (DPS)
    protected string DamageTextFormat = "{0} ({1})";

    public void Initialize(Entity entity)
    {
        ID = entity.Id;
        NameText.text = entity.Name;
        Bar.value = 0;
    }

    public void UpdateInfo(float damage, float highestDamage, float fightDuration)
    {
        DamageText.text = string.Format(DamageTextFormat, Numbers.Abbreviate(damage), Numbers.Abbreviate(damage / fightDuration));
        TargetBarValue = damage / highestDamage;
    }

    private void Update()
    {
        Bar.value = Mathf.Lerp(Bar.value, TargetBarValue, LerpConstant);
    }
}
