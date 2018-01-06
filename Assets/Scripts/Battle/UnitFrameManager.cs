using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrameManager : MonoBehaviour
{
    public GameObject unitFrameRef;

    public Raid Raid;

    private Transform Grid;

    private void Awake()
    {
        Grid = GetComponent<RectTransform>();
    }

    public void PopulateUnitFrames(Raid raid)
    {
        DeleteChildren();

        Raid = raid;

        if(Raid.Size == RaidSize.Large)
        {
            GetComponent<GridLayoutGroup>().constraintCount = 6;
        }
        else
        {
            GetComponent<GridLayoutGroup>().constraintCount = 4;
        }

        for(int i = 0; i < Raid.Raiders.Count; i++)
        {
            GameObject uf = Instantiate(unitFrameRef);
            uf.GetComponent<UnitFrame>().Initialize(Raid, i);
            uf.transform.SetParent(Grid.transform);
        }
    }

    private void DeleteChildren()
    {
        for (int i = Grid.childCount - 1; i >= 0; i--)
        {
            Destroy(Grid.GetChild(i).gameObject);
        }
    }
}
