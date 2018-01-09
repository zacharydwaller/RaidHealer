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
    public List<Entity> Raiders;

    public int NumTanks { get { return Raiders.Count(r => RoleOf(r) == Role.Tank); } }

    protected float HurtThreshold = 90.0f;

    protected BattleManager Mgr;

    public Raid(BattleManager mgr, Raider player, RaidSize _size)
    {
        Mgr = mgr;

        Size = _size;
        Raiders = new List<Entity>();

        BuildRaid(player);
    }

    /// <summary>
    /// Gets an alive tank provided with optional offset for tank switches
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Entity GetTank(int index = 0)
    {
        var aliveTanks = Raiders.Where(r => RoleOf(r) == Role.Tank && r.IsAlive).ToList();

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
    /// Gets the raider with the lowest health + healPredict. Can optionally ignore heal prediction
    /// </summary>
    /// <returns></returns>
    public Entity GetLowestHealth(bool ignoreHealPredict = false)
    {
        float minHP = Mathf.Infinity;
        Entity minRaider = null;
        foreach(var raider in Raiders)
        {
            float effectiveHealthPercent;

            if (ignoreHealPredict) effectiveHealthPercent = raider.HealthPercent;
            else effectiveHealthPercent = raider.HealthPercentPredict;

            if(raider.IsAlive && (effectiveHealthPercent < minHP))
            {
                minHP = raider.HealthPercent;
                minRaider = raider;
            }
        }

        // Everyone's health is full
        if (minHP == 100) return null;

        return minRaider;
    }

    /// <summary>
    /// Returns how many raiders in a list are injured lower than the hurtThreshold
    /// </summary>
    /// <param name="raiders"></param>
    /// <returns></returns>
    public int GetNumberHurt(IList<Entity> raiders, bool ignoreHealPredict = false)
    {
        System.Func<Entity, bool> predicate;
        if (ignoreHealPredict) predicate = r => r.HealthPercent < HurtThreshold;
        else predicate = r => r.HealthPercentPredict < HurtThreshold;

        return raiders.Count(predicate);
    }

    /// <summary>
    /// Gets the lowest index raider who is still alive
    /// TODO: Get raider with highest DPS (sort of like threat)
    /// </summary>
    /// <returns></returns>
    public Entity GetNextAlive()
    {
        return Raiders.FirstOrDefault(r => r.IsAlive);
    }

    //Get by ID
    public Entity GetByID(int id)
    {
        return Raiders.FirstOrDefault(r => r.ID == id);
    }

    public Entity GetRandom()
    {
        Entity raider;
        do
        {
            raider = Raiders[Random.Range(0, Raiders.Count)];
        } while (raider.IsDead);

        return raider;
    }

    public Coordinate GetCoordinate(Entity raider)
    {
        int index = Mgr.UFManager.GetRaiderIndex(raider);
        return IndexToCoord(index);
    }

    public Entity GetRaider(Coordinate coordinate)
    {
        int index = CoordToIndex(coordinate);
        return Mgr.UFManager.GetRaiderByIndex(index);
    }

    public IList<Entity> GetSplash(Entity centerRaider)
    {
        return GetSplash(GetCoordinate(centerRaider));
    }

    public IList<Entity> GetSplash(Coordinate center)
    {
        var list = new List<Entity>();

        for(int r = -1; r <= 1; r++)
        {
            for(int c = -1; c <= 1; c++)
            {
                Coordinate coord = new Coordinate(center.Row + r, center.Col + c);

                if(coord.Row >= 0 && coord.Row < RaidSizeUtil.GetRows(Size)
                    && coord.Col >= 0 && coord.Col < RaidSizeUtil.GetCols(Size))
                {
                    list.Add(GetRaider(coord));
                }
            }
        }

        return list;
    }

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
    protected void BuildRaid(Entity player)
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
        var raiders = new List<Entity>();
        for(int i = 0; i < number; i++)
        {
            raiders.Add(CreateRaider(role));
        }
        Raiders.InsertRange(index, raiders);
    }

    protected Entity CreateRaider(Role role)
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
        foreach(Entity raider in Raiders)
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

    public Role RoleOf(Entity ent)
    {
        var raider = ent as Raider;
        if (raider != null) return raider.Role;
        else return Role.Damage;
    }

    protected Coordinate IndexToCoord(int index)
    {
        int cols = RaidSizeUtil.GetCols(Size);
        return new Coordinate(index / cols, index % cols);
    }

    protected int CoordToIndex(Coordinate coord)
    {
        int cols = RaidSizeUtil.GetCols(Size);
        return coord.Row * cols + coord.Col;
    }
}
