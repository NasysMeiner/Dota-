using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private Dictionary<string, List<List<Stat>>> _users = new();
    private List<List<Stat>> _statsPlayer = new();
    private List<List<Stat>> _statsAi = new();

    public event UnityAction<string, int> AddStat;

    public void InitChangerStats()
    {

    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<List<Stat>> stats = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
            stats.Add(statsPrefab.WarriorData.Stats);

        _users.Add(name, stats);

        AddStat?.Invoke(name, dataUnitStats.StatsPrefab.Count);
    }

    public List<Stat> GetStatUnit(string name, int id)
    {
        return _users[name][id];
    }
}
