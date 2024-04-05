using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataUnitPrefab")]
public class DataUnitPrefab : ScriptableObject
{
    [Header("UnitPrefab")]
    public List<PrefabUnit> Prefabs = new List<PrefabUnit>();

    public Unit GetPrefab(TypeUnit typeUnit)
    {
        foreach (var unit in Prefabs)
            if (unit.TypeUnit == typeUnit)
                return unit.Prefab;

        return null;
    }
}
