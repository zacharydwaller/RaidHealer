using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitFrameManager UFManager;

    public Boss Boss;

    public RaidSize RaidSize;
    public Raid Raid;

    public Raider Player;

    private void Awake()
    {
        Raid = new Raid(this, Player, RaidSize);
        UFManager.PopulateUnitFrames(Raid);
        Boss = new TestBoss(this);
        Player.SetManager(this);
    }

    private void Update()
    {
        foreach(var raider in Raid.Raiders)
        {
            if(!raider.IsDead) raider.Tick();
        }

        if(!Boss.IsDead) Boss.Tick();
    }
}
