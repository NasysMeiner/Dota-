using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroupCustomizerData")]
public class GroupCustomizerData : ScriptableObject
{
    public List<Group> GroupUnits;

    private DataUnitStats _dataUnitStats;

    public void InitGroupPrice(DataUnitStats dataUnitStats)
    {
        _dataUnitStats = dataUnitStats;

        foreach (Group group in GroupUnits)
            group.CalculeitPriceGroup(dataUnitStats);
    }
}

[System.Serializable]
public class Group
{
    public TypeGroup TypeGroup;
    public List<VariertyUnit> VariertyUnits;

    public int PriceGroup { get; private set; }

    public void CalculeitPriceGroup(DataUnitStats dataUnitStats)
    {
        int price = 0;

        foreach (var unit in VariertyUnits)
            foreach(StatsPrefab statsPrefab in dataUnitStats.StatsPrefab)
                if(unit == statsPrefab.VariertyUnit)
                    price += statsPrefab.WarriorData.Price;

        PriceGroup = price;
    }
}

