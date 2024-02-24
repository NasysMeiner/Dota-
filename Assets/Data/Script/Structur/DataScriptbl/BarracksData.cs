using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BarracksData")]
public class BarracksData : ScriptableObject
{
    [Header("Spawn wait?")]
    public bool IsWait;

    [Header("Wave")]
    public SpawnerData SpawnerData;

    [Header("StructData")]
    public DataStructure DataStructure;

    [Header("UnitPrefab")]
    public DataUnitPrefab Prefabs;
    public DataUnitStats Stats;

    public List<StatsPrefab> StatsPrefab => Stats.StatsPrefab;

    private PointCreator _pointCreator;

    public PointCreator PointCreator => _pointCreator;

    public void WriteData(PointCreator pointCreator)
    {
        _pointCreator = pointCreator;
    }
}

[System.Serializable]
public class PrefabUnit
{
    public TypeUnit TypeUnit;
    public Unit Prefab;
}

[System.Serializable]
public class StatsPrefab
{
    public VariertyUnit VariertyUnit;
    public WarriorData WarriorData;
}