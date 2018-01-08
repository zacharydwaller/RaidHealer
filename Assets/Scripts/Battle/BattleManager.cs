using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitFrameManager UFManager;

    protected CombatLogger CombatLogger;
    protected DamageMeter DamageMeter;

    public Boss Boss;

    public RaidSize RaidSize;
    public Raid Raid;

    public Player Player;

    public float PowerLevel;

    private void Awake()
    {
        Player = new Player(this);
        Raid = new Raid(this, Player, RaidSize);
        Boss = new TestBoss(this);

        UFManager.PopulateUnitFrames(this);
        CombatLogger = GetComponent<CombatLogger>();
        DamageMeter = GetComponent<DamageMeter>();
    }

    private void Update()
    {
        foreach(var raider in Raid.Raiders)
        {
            if(raider.IsAlive) raider.Tick();
        }

        if(Boss.IsAlive) Boss.Tick();
    }

    public void LogLine(string message)
    {
        CombatLogger.LogLine(message);
    }

    public void LogAction(Entity user, string message)
    {
        CombatLogger.LogAction(user, message);
    }

    public void LogAction(Entity user, Entity target, Ability ability)
    {
        CombatLogger.LogAction(user, target, ability);
        DamageMeter.LogAction(user, target, ability);
    }
}
