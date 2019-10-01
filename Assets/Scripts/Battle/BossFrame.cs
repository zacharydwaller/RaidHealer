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

    public Slider CastBar;

    public Text AbilityText;
    public Text CastRemainingText;

    protected float LerpConstant = 0.3f;

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

        CastBar.value = 0;
        CastBar.gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        // Health
        HealthText.text = Numbers.Abbreviate(Boss.Health);
        HealthPText.text = string.Format("{0}%", Boss.HealthPercent.ToString("G3"));

        HealthBar.value = Mathf.Lerp(HealthBar.value, Boss.Health, LerpConstant);

        // Cast
        if(Boss.CastManager.IsCasting && Boss.CastManager.CurrentAbility.CastTime > 0.0f)
        {
            if(!CastBar.gameObject.activeInHierarchy)
            {
                CastBar.gameObject.SetActive(true);
            }

            if(AbilityText.text != Boss.CastManager.CurrentAbility.Name)
            {
                AbilityText.text = Boss.CastManager.CurrentAbility.Name;
            }

            CastBar.value = Mathf.Lerp(CastBar.value, Boss.CastManager.CastProgress, LerpConstant);
        }
        else if(!Boss.CastManager.IsCasting && CastBar.gameObject.activeInHierarchy)
        {
            CastBar.value = 0;
            CastBar.gameObject.SetActive(false);
        }

        // Enrage
        if(Boss.IsEnraged)
        {
            HealthBarFill.color = Color.white;
            NameText.color = Color.red;
        }
    }
}
