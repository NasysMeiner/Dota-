using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private readonly Dictionary<string, List<ContainerPack>> _users = new();

    public Dictionary<string, List<ContainerPack>> Users => _users;

    public void InitChangerStats()
    {

    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<ContainerPack> packs = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
        {
            ContainerPack containerPack = new();
            containerPack.LoadStats(statsPrefab.WarriorData.CurrentStats, statsPrefab.WarriorData.Stats, statsPrefab.WarriorData.Prices, statsPrefab.WarriorData.Name);
            packs.Add(containerPack);
        }

        _users.Add(name, packs);
    }

    public ContainerPack GetStatUnit(string name, int id)
    {
        return _users[name][id];
    }
}

[System.Serializable]
public class ContainerPack
{
    public string Name;
    public List<CurrentStat> CurrentStats = new();
    public List<Stat> Stats = new();
    public List<PriceStat> Prices = new();

    public void LoadStats(List<CurrentStat> currentStats, List<Stat> stats, List<PriceStat> prices, string name)
    {
        CurrentStats = currentStats;
        Stats = stats;
        Prices = prices;
        Name = name;
    }
}
