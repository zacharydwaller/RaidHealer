using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitFrameManager UFManager;

    public RaidSize raidSize;
    public Raid Raid;

    public Raider Player;

    private void Awake()
    {
        Raid = new Raid(Player, raidSize);
        UFManager.PopulateUnitFrames(Raid);
    }
}
