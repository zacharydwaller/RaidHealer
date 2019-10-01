using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMeter : MonoBehaviour
{
    public bool ShowBars = false;

    public GameObject DamageBarRef;

    public GameObject DamageMeterFrame;
    public Text RaidDpsText;

    protected Dictionary<int, float> DamageTable;
    protected Dictionary<int, DamageBar> DamageBars;

    protected float TotalDamage;

    // To calculate raid damage in 5 second steps
    protected float SetDamage;
    protected float SetDuration = 5.0f;
    protected float SetTime = 0.0f;

    // Update Meter on interval
    protected float UpdateDelay = 1f;
    protected float NextUpdate = 0f;

    protected float StartTime;
    protected BattleManager Mgr;

    protected float FightDuration { get { return Time.time - StartTime; } }

    private void Awake()
    {
        Mgr = GetComponent<BattleManager>();
        StartTime = Time.time;

        DamageTable = new Dictionary<int, float>();
        DamageBars = new Dictionary<int, DamageBar>();
        TotalDamage = 0;
    }

    private void Update()
    {
        if (TotalDamage == 0) return;

        SetTime += Time.deltaTime;

        if(Time.time >= NextUpdate)
        {
            NextUpdate = Time.time + UpdateDelay;

            // Update Raid DPS
            RaidDpsText.text = string.Format("Raid DPS: {0}", Numbers.Abbreviate(SetDamage / SetTime));

            if (SetTime >= SetDuration)
            {
                SetDamage = SetDamage / SetTime;
                SetTime = 1.0f;
            }

            // Update Damage Bars
            if(ShowBars)
            {
                float highestDamage = DamageTable.Max(d => d.Value);
                foreach (var damageBar in DamageBars.Values)
                {
                    UpdateDamageBar(damageBar, highestDamage);
                }
                SortBars();
            }
        }
    }

    public void LogAction(Entity user, Entity target, OldAbility ability)
    {
        // Return if attack/ability wasn't made against boss
        if (!target.Equals(Mgr.Boss)) return;

        float damage = user.AbilityPower * ability.PowerCoefficient;

        TotalDamage += damage;
        SetDamage += damage;

        if(!DamageTable.ContainsKey(user.Id))
        {
            DamageTable.Add(user.Id, 0);
            CreateDamageBar(user);
        }

        DamageTable[user.Id] += damage;
    }

    public void UpdateDamageBar(DamageBar bar, float highestDamage)
    {
        int id = bar.ID;
        float damage = DamageTable[id];

        bar.UpdateInfo(damage, highestDamage, FightDuration);
    }

    public void CreateDamageBar(Entity raider)
    {
        var barObj = Instantiate(DamageBarRef);
        barObj.transform.SetParent(DamageMeterFrame.transform, false);
        
        var barScript = barObj.GetComponent<DamageBar>();
        barScript.Initialize(raider);
        
        DamageBars.Add(raider.Id, barScript);
    }

    public void SortBars()
    {
        var list = DamageTable.ToList();
        list.Sort((e1, e2) => e2.Value.CompareTo(e1.Value));

        int i;
        for(i = 0; (i < 10) && i < list.Count; i++)
        {
            var bar = DamageBars[list[i].Key];
            bar.gameObject.SetActive(true);
            bar.transform.SetSiblingIndex(i);
        }
        for(; i < list.Count; i++)
        {
            var bar = DamageBars[list[i].Key];
            bar.gameObject.SetActive(false);
        }
    }
}
