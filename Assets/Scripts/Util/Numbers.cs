using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Numbers
{
    public static string BillionAbbrev = "B";
    public static string MillionAbbrev = "M";
    public static string ThousandAbbrev = "k";

    public static float Billion = 1000000000;
    public static float Million = 10000000;
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
}
