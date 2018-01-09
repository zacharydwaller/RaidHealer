using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    public float StartingItemLevel = 100;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerInfo = new PlayerInfo("Player", StartingItemLevel);
    }
}
