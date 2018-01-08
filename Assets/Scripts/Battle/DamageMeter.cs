using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMeter : MonoBehaviour
{
    public GameObject DamageBarRef;

    public GameObject DamageMeterFrame;
    public Text RaidDpsText;

    protected Dictionary<int, float> DamageTable;
    protected float TotalDamage;

    // To calculate raid damage in 5 second steps
    protected float SetDamage;
    protected float SetDuration = 5.0f;
    protected float SetTime = 0.0f;

    // Update Meter on interval
    protected float UpdateDelay = 0.33f;
    protected float NextUpdate = 0f;

    protected float StartTime;
    protected BattleManager Mgr;

    protected float FightDuration { get { return Time.time - StartTime; } }

    private void Awake()
    {
        Mgr = GetComponent<BattleManager>();
        StartTime = Time.time;

        DamageTable = new Dictionary<int, float>();
        TotalDamage = 0;
    }

    private void Update()
    {
        SetTime += Time.deltaTime;

        if(Time.time >= NextUpdate)
        {
            NextUpdate = Time.time + UpdateDelay;

            RaidDpsText.text = string.Format("Raid DPS: {0}", Numbers.Abbreviate(SetDamage / SetTime));

            if (SetTime >= SetDuration)
            {
                SetDamage = SetDamage / SetTime;
                SetTime = 1.0f;

                Debug.Log("Reset");
            }
        }
    }

    public void LogAction(Entity user, Entity target, Ability ability)
    {
        // Return if attack/ability wasn't made against boss
        if (!target.Equals(Mgr.Boss)) return;

        float damage = user.AbilityPower * ability.PowerCoefficient;

        TotalDamage += damage;
        SetDamage += damage;

        if(!DamageTable.ContainsKey(user.ID))
        {
            DamageTable.Add(user.ID, 0);
        }

        DamageTable[user.ID] += damage;
    }
}
