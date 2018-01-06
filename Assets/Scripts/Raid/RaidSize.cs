using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RaidSize
{
    Group = 4, Small = 12, Large = 24
}

public static class RaidSizeUtil
{
    public static int GetRows(RaidSize size)
    {
        switch(size)
        {
            case RaidSize.Group: return 1;
            case RaidSize.Small: return 3;
            case RaidSize.Large: return 4;
        }
        return 0;
    }

    public static int GetCols(RaidSize size)
    {
        return (int)size / GetRows(size);
    }
}