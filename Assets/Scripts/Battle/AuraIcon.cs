using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    public Aura Aura;

    public Image SpellImage;
    public Image CDMask;

    public void Initialize(Aura aura)
    {
        Aura = aura;
        SpellImage.enabled = true;
        SpellImage.sprite = Resources.Load<Sprite>(Aura.AuraEffect.ImagePath);
        UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        var auraProgress = 1.0f - Mathf.Max(Aura.ExpirationTime - Time.time, 0) / Aura.Duration;

        if (auraProgress < 1.0f)
        {
            CDMask.fillAmount = 1.0f - auraProgress;
        }

        if(Time.time >= Aura.ExpirationTime)
        {
            Destroy(this.gameObject);
        }
    }
}