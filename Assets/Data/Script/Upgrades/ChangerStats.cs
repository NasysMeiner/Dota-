using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private readonly Dictionary<string, List<ContainerPack>> _users = new();
    private readonly Dictionary<string, List<WarriorData>> _unitsStats = new();

    private Bank _bank;

    public Dictionary<string, List<ContainerPack>> Users => _users;

    public event UnityAction ChangeUnitStat;

    public void InitChangerStats(Bank bank)
    {
        _bank = bank;
    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<ContainerPack> packs = new();
        List<WarriorData> unitsStats = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
        {
            ContainerPack containerPack = new();
            containerPack.LoadStats(statsPrefab.WarriorData);
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

    public WarriorData GetWarriorData(string name, int id)
    {
        return _unitsStats[name][id];
    }

    public int GetPrice(ContainerPack containerPack)
    {
        int price;

        if (containerPack.Price.Count >= containerPack.Currentevel)
            price = containerPack.Price[containerPack.Currentevel];
        else
            price = containerPack.Price[containerPack.Price.Count - 1];

        return price;
    }

    public void IncreaseLevel(string name, int id)
    {
        ContainerPack containerPack = GetStatUnit(name, id);
        WarriorData data = _unitsStats[containerPack.Name][id];

        if (containerPack.CheckLevelUp())
        {
            if (_bank.Pay(GetPrice(containerPack), name))
            {
                if (data.LevelUp())
                {
                    containerPack.LoadStats(data);
                    ChangeUnitStat?.Invoke();
                }
            }
        }
    }
}

[System.Serializable]
public class ContainerPack
{
    public string Name;
    public int Currentevel;
    public int MaxLevel;
    public List<Stat> Stats = new();
    public List<int> Price = new();

    public void LoadStats(WarriorData warriorData)
    {
        Currentevel = warriorData.CurrentLevel;
        MaxLevel = warriorData.MaxLevel;
        Stats = warriorData.Stats;
        Price = warriorData.Prices;
        Name = warriorData.Name;
    }

    public bool CheckLevelUp()
    {
        return Currentevel < MaxLevel;
    }
}
