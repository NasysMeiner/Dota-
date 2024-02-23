using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour, IStructur, IEntity
{
    private float _healPoint;
    private bool _isAlive = true;
    private Trash _trash;

    private List<Barrack> _barracks;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public int Income { get; private set; }
    public int Money { get; private set; }
    public float MaxHealPoint { get; private set; }
    public Counter Counter { get; private set; }
    public Counter EnemyCounter { get; private set; }
    public string Name { get; private set; }
    public PointCreator PointCreator { get; private set; }

    public bool IsAlive => _isAlive;

    public event UnityAction<float> HealPointChange;

    private void OnDisable()
    {
        foreach(var barracks in _barracks)
            barracks.DestroyBarrack -= OnDestroyBarrack;
    }

    public void Destruct()
    {
        _isAlive = false;
        Counter.DeleteEntity(this);
        _trash.AddQueue(this);
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        HealPointChange?.Invoke(_healPoint);

        if (_healPoint <= 0)
            Destruct();
    }

    public void InitializeCastle(DataGameInfo dataGameInfo, List<Barrack> structurs, BarracksData dataStructureBarracks, Trash trash, PointCreator pointCreator)
    {
        InitializeStruct(dataGameInfo.DataStructure);

        Counter = new Counter();
        Money = dataGameInfo.StartMoney;
        Name = dataGameInfo.Name;
        dataGameInfo.DataStructure.Name = Name;
        dataStructureBarracks.DataStructure.Name = Name;
        _barracks = structurs;
        _trash = trash;
        PointCreator = pointCreator;

        for (int i = 0; i < structurs.Count; i++)
        {
            structurs[i].InitializeBarracks(dataStructureBarracks, Counter, trash);
            structurs[i].DestroyBarrack += OnDestroyBarrack;
        }
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        Income = dataStructure.Income;
        MaxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = MaxHealPoint;
    }

    public void InitializeEvent()
    {
        HealPointChange?.Invoke(_healPoint);
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        EnemyCounter = enemyCounter;

        foreach (var barrack in _barracks)
        {
            barrack.SetEnemyCounter(enemyCounter);
        }
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnDestroyBarrack()
    {
        Counter.AddEntity(this);
    }
}
