using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogger : MonoBehaviour
{
    public GameObject LogObjectRef;
    public GameObject ContentPanel;
    public ScrollRect Scroll;

    public int maxMessages;

    //public DamageMeter

    protected BattleManager Mgr;

    protected float StartTime;

    private void Awake()
    {
        Mgr = GetComponent<BattleManager>();
        StartTime = Time.time;
    }

    private void Update()
    {
        // Snap combat log to bottom
        Scroll.verticalNormalizedPosition = 0f;

        CleanLog();
    }

    protected void LogLine(string text)
    {
        var messageObject = Instantiate(LogObjectRef);
        messageObject.GetComponent<Text>().text = text;
        messageObject.transform.SetParent(ContentPanel.transform, false);
    }

    public void LogAction(Entity user, Entity target, Ability ability)
    {
        string message = CreateAbilityMessage(user, target, ability);
        LogLine(message);
    }

    protected string CreateAbilityMessage(Entity user, Entity target, Ability ability)
    {
        var sb = new StringBuilder();

        string colorTextFormat = "<color={0}>{1}</color> ";
        string color = "lightblue";

        // Append User's name
        if (user.Equals(Mgr.Player)) color = "cyan";
        else if (user.Equals(Mgr.Boss)) color = "red";

        sb.Append(string.Format(colorTextFormat, color, user.Name));

        // Append action
        sb.Append(ability.Name + " ");

        // Append Target's name
        if (target.Equals(Mgr.Player)) color = "cyan";
        else if (target.Equals(Mgr.Boss)) color = "red";
        else color = "lightblue";

        sb.Append(string.Format(colorTextFormat, color, target.Name));

        // Append ability magnitude
        float magnitude = user.AbilityPower * ability.PowerCoefficient;
        color = "yellow";

        sb.Append(string.Format(colorTextFormat, color, Numbers.Abbreviate(magnitude)));

        return sb.ToString();
    }

    protected void CleanLog()
    {
        int overflow = ContentPanel.transform.childCount - maxMessages;

        for(int i = 0; i < overflow; i++)
        {
            Destroy(ContentPanel.transform.GetChild(0).gameObject);
        }
    }
}
