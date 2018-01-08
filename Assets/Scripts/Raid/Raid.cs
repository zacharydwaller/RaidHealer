using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Raid
{
    public RaidSize Size;

    [SerializeField]
    public List<Raider> Raiders;

    public int NumTanks { get { return Raiders.Count(r => r.Role == Role.Tank); } }

    protected BattleManager Mgr;

    public Raid(BattleManager mgr, Raider player, RaidSize _size)
    {
        Mgr = mgr;

        Size = _size;
        Raiders = new List<Raider>();

        BuildRaid(player);
        //PrintRaid();
    }

    /// <summary>
    /// Gets an alive tank provided with optional offset for tank switches
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Raider GetTank(int index = 0)
    {
        var aliveTanks = Raiders.Where(r => r.Role == Role.Tank && r.IsAlive).ToList<Raider>();
        if (aliveTanks.Count > 0)
        {
            if(index < aliveTanks.Count)
            {
                return aliveTanks[index];
            }
            else
            {
                return aliveTanks[0];
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the raider with the lowest health
    /// </summary>
    /// <returns></returns>
    public Raider GetLowestHealth()
    {
        float minHP = Mathf.Infinity;
        Raider minRaider = null;
        foreach(var raider in Raiders)
        {
            if(raider.IsAlive && raider.HealthPercent < minHP)
            {
                minHP = raider.HealthPercent;
                minRaider = raider;
            }
        }

        return minRaider;
    }

    /// <summary>
    /// Gets the lowest index raider who is still alive
    /// TODO: Get raider with highest DPS (sort of like threat)
    /// </summary>
    /// <returns></returns>
    public Raider GetNextAlive()
    {
        return Raiders.FirstOrDefault(r => r.IsAlive);
    }

    //Get by ID
    public Raider GetByID(int id)
    {
        return Raiders.FirstOrDefault(r => r.ID == id);
    }

    // GetSingle(row, col)
    // Returns Raider

    // Get Splash (row, col)
    // Returns IList<Raider>

    // Get Chain(row, col, number)
    // Returns IList<Raider>

    // Get Smart AoE(int number)
    // Returns IList<Raider>

    // Get AoE
    // Returns IList<Raider>

    /*
     * Builds a raid layout that makes Splash/Chain heals work better
     * 
     * Group: 4, 1/1/2
     * Small: 12, 2/2/8
     * Large: 24, 2/4/18
     * 
     * Group
     * D T H D
     * 
     * Small
     * D T T D
     * D D D D
     * D H H D
     * 
     * Large
     * D D T T D D
     * D D D D D D
     * D D D D D D
     * D H H H H D
     */
    protected void BuildRaid(Raider player)
    {
        if(Size == RaidSize.Group)
        {
            AddRaiders(Role.Damage, 2, 0);
            AddRaiders(Role.Tank, 1, 1);
            Raiders.Insert(2, player);
        }
        else if(Size == RaidSize.Small)
        {
            AddRaiders(Role.Damage, 8, 0);
            AddRaiders(Role.Tank, 2, 1);
            AddRaiders(Role.Healer, 1, 9);
            Raiders.Insert(9, player);
        }
        else if(Size == RaidSize.Large)
        {
            AddRaiders(Role.Damage, 18, 0);
            AddRaiders(Role.Tank, 2, 2);
            AddRaiders(Role.Healer, 3, 19);
            Raiders.Insert(19, player);
        }
    }

    protected void AddRaiders(Role role, int number, int index)
    {
        var raiders = new List<Raider>();
        for(int i = 0; i < number; i++)
        {
            raiders.Add(CreateRaider(role));
        }
        Raiders.InsertRange(index, raiders);
    }

    protected Raider CreateRaider(Role role)
    {
        if (role == Role.Tank) return new Tank(Mgr);
        else if (role == Role.Healer) return new Healer(Mgr);
        else return new Damage(Mgr);
    }

    protected void PrintRaid()
    {
        StringBuilder sb = new StringBuilder();
        int numCols = RaidSizeUtil.GetCols(Size);
        int col = 0;
        foreach(Raider raider in Raiders)
        {
            sb.Append(raider.Name + "\t");
            col++;
            if (col == numCols)
            {
                col = 0;
                Debug.Log(sb.ToString());
                sb = new StringBuilder();
            }
        }
    }
}
