using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Numbers
{
    public static string BillionAbbrev = "B";
    public static string MillionAbbrev = "M";
    public static string ThousandAbbrev = "k";

    public static float Billion = 1000000000;
    public static float Million = 1000000;
    public static float Thousand = 1000;

    public static string Abbreviate(float input)
    {
        string ret;

        if (input >= Billion)
        {
            input /= Billion;
            ret = input.ToString("G3");
            ret += BillionAbbrev;
        }
        else if (input >= Million)
        {
            input /= Million;
            ret = input.ToString("G3");
            ret += MillionAbbrev;
        }
        else if (input >= Thousand)
        {
            input /= Thousand;
            ret = input.ToString("G3");
            ret += ThousandAbbrev;
        }
        else
        {
            ret = input.ToString("G3");
        }

        return ret;
    }

    /// <summary>
    /// Returns the number increased by percent. Percent is 100-based, for instance enter 150.0f for 150.0% increase
    /// </summary>
    /// <param name="number"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static float IncreaseByPercent(float number, float percent)
    {
        return number * (1 + (percent / 100.0f));
    }

    /// <summary>
    /// Returns the number decreased by percent. Percent is 100-based, for instance enter 150.0f for 150.0% increase
    /// </summary>
    /// <param name="number"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static float DecreaseByPercent(float number, float percent)
    {
        return number * (1 - (percent / 100.0f));
    }
}
