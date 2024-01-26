using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barrack : MonoBehaviour, IStructur, IEntity
{
    private Transform _spawnPoint;
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;

    private Path _path;
    private Counter _counter;
    private Counter _enemyCounter;
    private Trash _trash;

    private Warrior _unitPrefab;

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;
    public GameObject GameObject => gameObject;
    public Vector3 Position => transform.position;

    public event UnityAction DestroyBarracks;

    public void Destruct()
    {
        DestroyBarracks?.Invoke();
        _counter.DeleteEntity(this);
        _trash.AddQueue(this);
    }

    public void InitializeStruct(DataStructure dataStructure, string name)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;
        _name = name;

        _healPoint = _maxHealPoint;
        _spawnPoint = transform.GetChild(0);
    }

    public void InitializeBarracks(Path path, Counter counter, Warrior unitPrefab, Trash trash)
    {
        _path = path;
        _counter = counter;
        _unitPrefab = unitPrefab;
        _trash = trash;
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
        int unit = 20;
        float timeSpawn = 1.5f;

        StartCoroutine(Spawn(timeSpawn, unit));
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator Spawn(float time, int units)
    {
        int id = 0;

        while (true)
        {
            if (units <= 0)
                break;

            Warrior newUnit = Instantiate(_unitPrefab);
            newUnit.Died += OnDied;
            newUnit.SetPosition(transform.position);
            _counter.AddEntity(newUnit);
            newUnit.InitUnit(_path, _enemyCounter, id++, Name);
            units--;

            yield return new WaitForSeconds(time);
        }
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        _counter.DeleteEntity(unit);
        //unit.enabled = false;
        _trash.AddQueue(unit);
    }
}
