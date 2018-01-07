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
        Boss = new TestBoss(this);
        Player.SetManager(this);

        UFManager.PopulateUnitFrames(this);
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
