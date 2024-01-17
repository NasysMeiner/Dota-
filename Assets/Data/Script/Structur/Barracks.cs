using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barracks : MonoBehaviour, IStructur, IEntity
{
    private Transform _spawnPoint;
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;

    private Path _path;
    private Counter _counter;
    private Counter _enemyCounter;

    private Warrior _unitPrefab;

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;

    public Vector3 Position => transform.position;

    public event UnityAction DestroyBarracks;

    public void Destruct()
    {
        DestroyBarracks?.Invoke();
        _counter.DeleteEntity(this);
    }

    public void InitializeStruct(DataStructure dataStructure, string name)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = _maxHealPoint;
        _spawnPoint = transform.GetChild(0);
    }

    public void InitializeBarracks(Path path, Counter counter, Warrior unitPrefab)
    {
        _path = path;
        _counter = counter;
        _unitPrefab = unitPrefab;
        _counter.AddEntity(this);
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        _enemyCounter = enemyCounter;
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
            Destruct();
    }

    public int GetIncome()
    {
        return _income;
    }

    public void SpawnUnits()
    {
        int unit = 1;
        float timeSpawn = 1.5f;

        StartCoroutine(Spawn(timeSpawn, unit));
    }

    private IEnumerator Spawn(float time, int units)
    {
        while (true)
        {
            if (units <= 0)
                break;

            Warrior newUnit = Instantiate(_unitPrefab);
            newUnit.Died += OnDied;
            newUnit.transform.position = _spawnPoint.position;
            _counter.AddEntity(newUnit);
            newUnit.InitUnit(_path, _enemyCounter);
            units--;

            yield return new WaitForSeconds(time);
        }
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        _counter.DeleteEntity(unit);
        Destroy(unit);
    }
}
