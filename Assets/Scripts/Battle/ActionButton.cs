using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public KeyCode Key;
    public int AbilityIndex;

    protected BattleManager Mgr;

    private void Start()
    {
        Mgr = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(Key))
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
