using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    public Image Background;
    public Slider Bar;

    protected Player Player;

    protected bool IsShowing { get { return Background.enabled; } }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>().Player;

        Background.enabled = false;
        Bar.value = 0;
    }

    private void Update()
    {
        // Start Cast
        if(!IsShowing && Player.IsCasting)
        {
            Background.enabled = true;
        }

        // During Cast
        if(IsShowing)
        {
            Bar.value = Player.CurrentAbility.CastProgress;
        }

        // End Cast
        if(IsShowing && !Player.IsCasting)
        {
            Background.enabled = false;
            Bar.value = 0;
        }
    }
}
