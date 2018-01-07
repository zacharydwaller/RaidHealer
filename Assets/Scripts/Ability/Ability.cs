using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public string Name;
    public float CastTime;
    public float PowerCoefficient;

    public virtual void Do(Entity target, float power) { }
}
