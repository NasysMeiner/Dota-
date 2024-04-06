using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
    public List<int> IdLine;
    public Group TestGroup;
    public float TimeSpawn = 1f;

    private DataUnitStats _dataUnitStatsAi;
    private DataUnitPrefab _dataUnitPrefab;
    private Castle _castle;
    private Trash _trash;
    private SelectorPointSpawner _selectorPointSpawner;

    public void InitAutoSpawner(DataUnitStats dataUnitStats, Castle castle, DataUnitPrefab dataUnitPrefab, Trash trash, SelectorPointSpawner selectorPointSpawner)
    {
        _dataUnitStatsAi = dataUnitStats;
        _dataUnitPrefab = dataUnitPrefab;
        _castle = castle;
        _trash = trash;
        _selectorPointSpawner = selectorPointSpawner;
    }

    public void StartSpawn(/*Group groupUnit, Transform point*/)
    {
        StartCoroutine(SpawnUnit(TestGroup, IdLine[1]));
    }

    public IEnumerator SpawnUnit(Group groupUnit, int idLine)
    {
        int maxSpawnUnit = groupUnit.VariertyUnits.Count;
        int spawnUniy = 0;

        while (spawnUniy < maxSpawnUnit)
        {
            StatsPrefab currentUnit = _dataUnitStatsAi.GetStatsPrefab(TestGroup.VariertyUnits[spawnUniy]);
            Vector3 newPointt;

            newPointt = _selectorPointSpawner.GetPointSpawn(currentUnit.TypeUnit, idLine);

            Unit newUnit = Instantiate(_dataUnitPrefab.GetPrefab(currentUnit.TypeUnit));
            _castle.Counter.AddEntity(newUnit);
            _trash.WriteUnit(newUnit);
            newUnit.LoadStats(currentUnit.WarriorData);
            newUnit.InitUnit(_castle.PointCreator.CreateRangePoint, _castle.EnemyCounter, 100, _castle.Name);
            newUnit.ChangePosition(newPointt);

            spawnUniy++;

            yield return new WaitForSeconds(TimeSpawn);
        }
    }
}
