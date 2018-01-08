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

    public void LogAction(Entity user, string action)
    {
        string message = GetNameString(user);
        message += action;
        MessageBatch.Add(message);
    }

    public void LogAction(Entity user, Entity target, Ability ability)
    {
        string message = CreateAbilityMessage(user, target, ability);
        MessageBatch.Add(message);
    }

    protected string CreateAbilityMessage(Entity user, Entity target, Ability ability)
    {
        var sb = new StringBuilder();
        float magnitude = user.AbilityPower * ability.PowerCoefficient;

        sb.Append(GetNameString(user));
        sb.Append(ability.Name + " ");
        sb.Append(GetNameString(target));
        sb.Append(GetColoredText(Numbers.Abbreviate(magnitude), "yellow"));

        return sb.ToString();
    }

    protected string GetNameString(Entity entity)
    {
        string colorTextFormat = "<color={0}>{1}</color> ";
        string color = "lightblue";

        if (entity.Equals(Mgr.Player)) color = "cyan";
        else if (entity.Equals(Mgr.Boss)) color = "red";

        return string.Format(colorTextFormat, color, entity.Name);
    }

    protected string GetColoredText(string message, string color)
    {
        string colorTextFormat = "<color={0}>{1}</color> ";
        return string.Format(colorTextFormat, color, message);
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
