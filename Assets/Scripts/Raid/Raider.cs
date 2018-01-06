using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Raider
{
    public string Name;

    public float MaxHealth;
    public float Health;

    public Role Role;

    public static float AbilityDelay;
    public float AbilityTime;

    public float AbilityPower = 1000;

    protected float baseHealth = 5000;
    protected float healthStdDev = 500;

    protected float baseAP = 1000;
    protected float apStdDev = 50;

    protected float baseAD = 1.25f;
    protected float adStdDev = 0.10f;

    public Raider()
    {
        Health = Distribution.GetRandom(baseHealth, healthStdDev);
        AbilityPower = Distribution.GetRandom(baseAP, apStdDev);
        AbilityDelay = Distribution.GetRandom(baseAD, adStdDev);

        AbilityTime = AbilityDelay;
    }
}
