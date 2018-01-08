using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLogger : MonoBehaviour
{
    public Text CombatLog;
    public ScrollRect Scroll;

    public int maxMessages;

    protected List<string> MessageBatch;
    protected float BatchWriteDelay = 0.33f;
    protected float NextBatchWrite = 0;

    protected BattleManager Mgr;

    private void Awake()
    {
        Mgr = GetComponent<BattleManager>();
        MessageBatch = new List<string>();
    }

    private void Update()
    {
        if(Time.time >= NextBatchWrite)
        {
            NextBatchWrite = Time.time + BatchWriteDelay;
            ProcessMessageBatch();
        }
    }

    public void LogLine(string text)
    {
        MessageBatch.Add(text);
    }

    public void LogAction(Entity user, Entity target, Ability ability)
    {
        string message = CreateAbilityMessage(user, target, ability);
        MessageBatch.Add(message);
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

    protected void ProcessMessageBatch()
    {
        CleanMessages();
        CombatLog.text = "";

        foreach (string message in MessageBatch)
        {
            CombatLog.text += message;
            CombatLog.text += "\n";
        }

        // Snap combat log to bottom
        Scroll.verticalNormalizedPosition = 0f;
    }

    protected void CleanMessages()
    {
        int overflow = MessageBatch.Count - maxMessages;

        if(overflow > 0)
        {
            MessageBatch.RemoveRange(0, overflow);
        }
    }
}
