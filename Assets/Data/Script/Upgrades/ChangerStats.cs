using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerStats : MonoBehaviour
{
    private readonly Dictionary<string, List<WarriorData>> _unitsStats = new();

    private Bank _bank;

    public event UnityAction ChangeUnitStat;

    public void InitChangerStats(Bank bank)
    {
        _bank = bank;
    }

    public void AddUnitStat(string name, DataUnitStats dataUnitStats)
    {
        List<WarriorData> unitsStats = new();

        foreach (StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
            unitsStats.Add(statsPrefab.WarriorData);

        _unitsStats.Add(name, unitsStats);
    }

    public WarriorData GetWarriorData(string name, int id)
    {
        return _unitsStats[name][id];
    }

    public void IncreaseLevelUnit(string name, int id)
    {
        WarriorData warrior = _unitsStats[name][id];

        if (warrior.CheckLevelUp())
        {
            if(_bank.Pay(warrior.GetPriceLevel(), name))
            {
                warrior.LevelUp();
                ChangeUnitStat?.Invoke();
            }
        }
    }

    public void UnlockSkill(string name, int id, int idSkill)
    {
        Debug.Log(name);
        WarriorData warrior = _unitsStats[name][id];

        if (warrior.CheckUnlockSkill(idSkill))
        {
            if(_bank.Pay(warrior.GetPriceSkill(idSkill), name))
            {
                warrior.UnlockSkill(idSkill);
                ChangeUnitStat?.Invoke();
            }
        }
    }
}
