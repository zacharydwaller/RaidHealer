using System;
using System.Collections;
using System.Collections.Generic;

// Does an okay job simulating a normal distribution by rolling 5d6
public static class Distribution
{
    static int dice = 5;
    static int sides = 6;

    static Random rand = new Random();

    public static float GetRandom(float mean, float stdDev)
    {
        int roll =  Roll(dice, sides);
        float zValue = RollToZ(dice, sides, roll);

        // z = (x - mu)/sigma
        // => x = z*sigma + mu
        return zValue * stdDev + mean;
    }

    public static int Roll(int dice, int sides)
    {
        int total = 0;
        for(int i = 0; i < dice; i++)
        {
            total += rand.Next(1, sides + 1);
        }
        return total;
    }

    public static float RollToZ(int dice, int sides, int roll)
    {
        float min = dice;
        float max = dice * sides;

        float adjustedMedian = (max - min) / 2.0f;
        float adjustedRoll = roll - min - adjustedMedian;

        float maxZ = 2.50f;
        float zStep = maxZ / adjustedMedian;

        float zValue = zStep * adjustedRoll;

        return zValue;
    }
}
