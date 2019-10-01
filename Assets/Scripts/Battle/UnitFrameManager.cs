using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrameManager : MonoBehaviour
{
    public GameObject unitFrameRef;

    public Transform BossFrame;
    public Transform RaidFrames;

    protected BattleManager Mgr;

    private void Awake()
    {
        
    }

    /// <summary>
    /// Gets a raider by child index - NOT the same as Entity ID
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Raider GetRaiderByIndex(int index)
    {
        return RaidFrames.GetChild(index).GetComponent<UnitFrame>().Raider;
    }

    public int GetRaiderIndex(Entity raider)
    {
        var list = RaidFrames.GetComponentsInChildren<UnitFrame>();
        
        for(int i = 0; i < list.Length; i++)
        {
            if(list[i].Raider.Id == raider.Id)
            {
                return i;
            }
        }

        return -1;
    }

    public void UnselectAll()
    {
        foreach(var uf in RaidFrames.GetComponentsInChildren<UnitFrame>())
        {
            uf.UnSelect();
        }
    }

    public void PopulateUnitFrames(BattleManager mgr)
    {
        Mgr = mgr;

        // Init Boss Frame
        BossFrame.GetComponent<BossFrame>().Initialize(Mgr.Boss);

        // Populate Raid Frames
        DeleteRaidFrames();

        Raid raid = Mgr.Raid;

        var grid = RaidFrames.GetComponent<GridLayoutGroup>();
        if(raid.Size == RaidSize.Large)
        {
            grid.constraintCount = 6;
        }
        else
        {
            grid.constraintCount = 4;
        }

        for(int i = 0; i < raid.Raiders.Count; i++)
        {
            GameObject uf = Instantiate(unitFrameRef);
            uf.GetComponent<UnitFrame>().Initialize(Mgr, i);
            uf.transform.SetParent(RaidFrames.transform);
            uf.transform.localScale = Vector3.one;
        }
    }

    private void DeleteRaidFrames()
    {
        for (int i = RaidFrames.childCount - 1; i >= 0; i--)
        {
            Destroy(RaidFrames.GetChild(i).gameObject);
        }
    }
}
