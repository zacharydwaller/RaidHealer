using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFrame : MonoBehaviour
{
    public Boss Boss;

    public Text NameText;
    public Text HealthText;
    public Text HealthPText;

    public Slider HealthBar;
    public Image HealthBarFill;

    protected float HealthLerpConstant = 0.3f;

    private void Awake()
    {
        
    }

    private void Update()
    {
        UpdateInfo();
    }

    public void Initialize(Boss boss)
    {
        Boss = boss;

        NameText.text = Boss.Name;
        HealthBar.maxValue = Boss.MaxHealth;
    }

    public void UpdateInfo()
    {
        HealthText.text = Numbers.Abbreviate(Boss.Health);
        HealthPText.text = string.Format("{0}%", Boss.HealthPercent.ToString("G3"));

        HealthBar.value = Mathf.Lerp(HealthBar.value, Boss.Health, HealthLerpConstant);

        if(Boss.IsEnraged)
        {
            HealthBarFill.color = Color.white;
            NameText.color = Color.red;
        }
    }
}
