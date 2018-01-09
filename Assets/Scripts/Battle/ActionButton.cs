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

    public Text KeyText;
    public Text SpellText;

    protected BattleManager Mgr;

    protected bool ModifierDown { get { return InputExt.GetModifier() == Modifier; } }

    private void Start()
    {
        Mgr = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        KeyText.text = InputExt.ToPrettyString(Key, Modifier);

        if(AbilityIndex >= 0 && AbilityIndex < Mgr.Player.AbilityList.Count)
        {
            SpellText.text = Mgr.Player.AbilityList[AbilityIndex].Name;
        }
        else
        {
            SpellText.text = "";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(Key) && ModifierDown)
        {
            Mgr.Player.AbilityPressed(AbilityIndex);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Mgr.Player.AbilityPressed(AbilityIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
