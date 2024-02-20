using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour, IStructur, IEntity
{
    private float _healPoint;
    private Trash _trash;

    private List<Barrack> _barracks;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public int Income { get; private set; }
    public int Money { get; private set; }
    public float MaxHealPoint { get; private set; }
    public Counter Counter { get; private set; }
    public string Name { get; private set; }

    public event UnityAction<float> HealPointChange;

    private void OnDisable()
    {
        foreach(var barracks in _barracks)
            barracks.DestroyBarrack -= OnDestroyBarrack;
    }

    public void Destruct()
    {
        Counter.DeleteEntity(this);
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        HealPointChange?.Invoke(_healPoint);

        if (_healPoint <= 0)
            Destruct();
    }

    public void InitializeCastle(DataGameInfo dataGameInfo, List<Barrack> structurs, List<Tower> towers, BarracksData dataStructureBarracks, Trash trash)
    {
        InitializeStruct(dataGameInfo.DataStructure);

        Counter = new Counter();
        Money = dataGameInfo.StartMoney;
        Name = dataGameInfo.name;
        _barracks = structurs;
        _trash = trash;

        for (int i = 0; i < structurs.Count; i++)
        {
            structurs[i].InitializeBarracks(dataStructureBarracks, Counter, trash);
            structurs[i].DestroyBarrack += OnDestroyBarrack;
        }

        if (towers!=null)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                towers[i].Initialization(Counter, trash);
            }
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
