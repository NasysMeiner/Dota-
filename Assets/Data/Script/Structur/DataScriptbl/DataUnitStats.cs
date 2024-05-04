using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataUnitStats")]
public class DataUnitStats : ScriptableObject
{
    public List<StatsPrefab> StatsPrefab = new();

    public StatsPrefab GetStatsPrefab(VariertyUnit variertyUnit)
    {
        foreach (var item in StatsPrefab)
            if(item.VariertyUnit == variertyUnit)
                return item;

        return null;
    }
}
