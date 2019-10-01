using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public KeyCode Key;
    public KeyCode Modifier;
    public int AbilityIndex;

    protected Ability Ability;

    public Text KeyText;
    public Text SpellText;

    public Image SpellImage;
    public Image CDMask;

    protected BattleManager Mgr;

    protected bool ModifierDown { get { return InputExt.GetModifier() == Modifier; } }
    protected bool HasAbility { get { return Ability != null; } }

    private void Start()
    {
        Mgr = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        KeyText.text = InputExt.ToPrettyString(Key, Modifier);

        Ability = Mgr.Player.GetAbility(AbilityIndex);
        if (HasAbility)
        {
            SpellText.text = Ability.Name;
            SpellImage.enabled = true;
            SpellImage.sprite = Resources.Load<Sprite>(Ability.ImagePath);
        }
        else
        {
            SpellText.text = "";
            SpellImage.enabled = false;
        }

        CDMask.fillAmount = 0;
    }

    private void Update()
    {
        if (HasAbility)
        {
            CheckPress();
            CheckCD();
        }
    }

    protected void CheckPress()
    {
        if (Input.GetKeyDown(Key) && ModifierDown)
        {
            Mgr.Player.AbilityPressed(AbilityIndex);
        }
    }

    protected void CheckCD()
    {
        if (!Mgr.Player.Cooldowns.ContainsKey(Ability.Name)) Mgr.Player.Cooldowns[Ability.Name] = 0.0f;

        var cdFinish = Mgr.Player.Cooldowns[Ability.Name];
        var cdProgress = 1.0f - Mathf.Max(cdFinish - Time.time, 0) / Ability.Cooldown;

        //Call SnapCDFill inside if statements to avoid dirtying canvas and forcing unnecessary draws
        if (cdProgress >= 0.0f)
        {
            CDMask.fillAmount = 1.0f - cdProgress;
            SnapCDFill();
        }
        // Check player GCD
        else if (!Mgr.Player.CastManager.GCDReady)
        {
            CDMask.fillAmount = 1.0f - Mgr.Player.CastManager.GCDProgress;
            SnapCDFill();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Mgr.Player.AbilityPressed(AbilityIndex);
    }

    public void SnapCDFill()
    {
        if (CDMask.fillAmount - 0.05f <= 0) CDMask.fillAmount = 0.0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
