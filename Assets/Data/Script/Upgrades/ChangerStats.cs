using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private Dictionary<string, List<ContainerPack>> _users = new();
    private List<List<Stat>> _statsPlayer = new();
    private List<List<Stat>> _statsAi = new();

    public event UnityAction<string, int> AddStat;

    public void InitChangerStats()
    {

    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<ContainerPack> packs = new();
        ContainerPack containerPack = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
        {
            containerPack.LoadStats(statsPrefab.WarriorData.CurrentStats, statsPrefab.WarriorData.Stats, statsPrefab.WarriorData.Prices);
            packs.Add(containerPack);
        }

        _users.Add(name, packs);

        AddStat?.Invoke(name, dataUnitStats.StatsPrefab.Count);
    }

    public ContainerPack GetStatUnit(string name, int id)
    {
        return _users[name][id];
    }
}

[System.Serializable]
public class ContainerPack
{
    public List<CurrentStat> CurrentStats = new();
    public List<Stat> Stats = new();
    public List<PriceStat> Prices = new();

    public void LoadStats(List<CurrentStat> currentStats, List<Stat> stats, List<PriceStat> prices)
    {
        CurrentStats = currentStats;
        Stats = stats;
        Prices = prices;
    }
}
