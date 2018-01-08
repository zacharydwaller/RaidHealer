using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitFrameManager UFManager;
    [HideInInspector]
    public CombatLogger CombatLogger;

    public Boss Boss;

    public RaidSize RaidSize;
    public Raid Raid;

    public Player Player;

    private void Awake()
    {
        Player = new Player(this);
        Raid = new Raid(this, Player, RaidSize);
        Boss = new TestBoss(this);

        UFManager.PopulateUnitFrames(this);
        CombatLogger = GetComponent<CombatLogger>();
    }

    private void Update()
    {
        foreach(var raider in Raid.Raiders)
        {
            if(raider.IsAlive) raider.Tick();
        }

        if(Boss.IsAlive) Boss.Tick();
    }
}
