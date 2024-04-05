using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    [SerializeField] private List<Point> _points = new();
    [SerializeField] private DataUnitStats _unitStats;
    [SerializeField] private DataUnitPrefab _prefab;
    [SerializeField] private Castle _aiCastle;

    public void SpawnUnit(int idPoint, VariertyUnit variertyUnit, TypeUnit typeUnit)
    {
        Unit unit;

        foreach(StatsPrefab statsPrefab in _unitStats.StatsPrefab)
        {
            if (statsPrefab.VariertyUnit == variertyUnit && statsPrefab.TypeUnit == typeUnit)
            {
                unit = Instantiate(_prefab.GetPrefab(typeUnit));
                //unit.
            }
        }
    }
}
