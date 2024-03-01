using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private readonly Dictionary<string, List<ContainerPack>> _users = new();
    private readonly Dictionary<string, List<WarriorData>> _unitsStats = new();


    public Dictionary<string, List<ContainerPack>> Users => _users;

    public event UnityAction ChangeUnitStat;

    public void InitChangerStats()
    {

    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<ContainerPack> packs = new();
        List<WarriorData> unitsStats = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
        {
            ContainerPack containerPack = new();
            containerPack.LoadStats(statsPrefab.WarriorData.CurrentStats, statsPrefab.WarriorData.Stats, statsPrefab.WarriorData.Prices, statsPrefab.WarriorData.Name);
            packs.Add(containerPack);
            unitsStats.Add(statsPrefab.WarriorData);
        }

        _users.Add(name, packs);
        _unitsStats.Add(name, unitsStats);
    }

    public ContainerPack GetStatUnit(string name, int id)
    {
        return _users[name][id];
    }

    public void IncreaseLevel(string name, int idStat, int id)
    {
        ContainerPack containerPack = GetStatUnit(name, id);
        bool isSuccess = containerPack.IncreaseLevel(idStat);

        if (isSuccess)
        {
            WarriorData warriorData = _unitsStats[name][id];
            warriorData.IncreaseLevel(containerPack);
            ChangeUnitStat?.Invoke();
        }
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

    public bool IncreaseLevel(int idStat)
    {
        CurrentStat currentStat = CurrentStats[idStat];
        Stat stat = Stats[idStat];

        if (currentStat.CurrentLevel + 1 <= stat.Levels.Count)
        {
            currentStat.CurrentLevel = currentStat.CurrentLevel + 1;

            return true;
        }

        return false;
    }
}
