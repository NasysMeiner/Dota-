using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour, IStructur, IEntity
{
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;
    private int _money;
    private Counter _counter;
    private Trash _trash;

    private List<Barrack> _barracks;

    public string Name => _name;
    public int Income => _income;
    public float MaxHealPoint => _maxHealPoint;
    public Counter Counter => _counter;

    public Vector3 Position => transform.position;

    public GameObject GameObject => gameObject;

    public event UnityAction<float> HealPointChange;

    private void OnDisable()
    {
        foreach(var barracks in _barracks)
            barracks.DestroyBarracks -= IsDestructBarracks;
    }

    public void Destruct()
    {
        _counter.DeleteEntity(this);
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        HealPointChange?.Invoke(_healPoint);

        if (_healPoint <= 0)
            Destruct();
    }

    public void InitializeCastle(DataStructure dataStructureCastle, DataGameInfo dataGameInfo, List<Barrack> structurs, DataStructure dataStructureBarracks, List<Path> paths, Warrior unitPrefab, Trash trash)
    {
        InitializeStruct(dataStructureCastle, dataGameInfo.name);

        _counter = new Counter();
        _money = dataGameInfo.StartMoney;
        _barracks = structurs;
        _trash = trash;

        for (int i = 0; i < structurs.Count; i++)
        {
            structurs[i].InitializeStruct(dataStructureBarracks, dataGameInfo.name);
            structurs[i].InitializeBarracks(paths[i], _counter, unitPrefab, trash);
            structurs[i].DestroyBarracks += IsDestructBarracks;
        }
    }

    public void InitializeStruct(DataStructure dataStructure, string name)
    {
        _income = dataStructure.Income;
        _name = name;
        _maxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = _maxHealPoint;
    }

    public void InitializeEvent()
    {
        HealPointChange?.Invoke(_healPoint);
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        foreach (var barrack in _barracks)
        {
            barrack.SetEnemyCounter(enemyCounter);
        }
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    private void IsDestructBarracks()
    {
        _counter.AddEntity(this);
    }
}
