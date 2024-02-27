using System.Collections.Generic;
using UnityEngine;

public class UnitStatsBlock : MonoBehaviour
{
    [SerializeField] private List<TypeStat> _types = new(); 
    [SerializeField] private StatView _healthPoint;
    [SerializeField] private StatView _attack;
    [SerializeField] private StatView _attackSpeed;

    private int _id;

    public void InitUnitStatsView(int id)
    {
        _id = id;
    }

    public void UpdateValuesStats(StatsContainer statsContainer)
    {
        _healthPoint.UpdateValues(statsContainer.Health);
        _attack.UpdateValues(statsContainer.Attack);
        _attackSpeed.UpdateValues(statsContainer.AttackSpeed);
    }

    public void UpdateActiveButton(int money)
    {
        _healthPoint.UpdateActiveMoney(money);
        _attack.UpdateActiveMoney(money);
        _attackSpeed.UpdateActiveMoney(money);
    }

    public void IncreaseLevel(int typeStat)
    {

    }
}

[System.Serializable]
public class StatsContainer
{
    public StatContainer Health { get; private set; }
    public StatContainer Attack { get; private set; }
    public StatContainer AttackSpeed { get; private set; }

    public StatsContainer()
    {
        Health = new StatContainer();
        Attack = new StatContainer();
        AttackSpeed = new StatContainer();
    }
}

[System.Serializable]
public class StatContainer
{
    public float CurrentLevel { get; private set; }
    public float NextLevel { get; private set; }
    public int Price { get; private set; }

    public void LoadStat(float currentLevel, float nextLevel, int price)
    {
        CurrentLevel = currentLevel;
        NextLevel = nextLevel;
        Price = price;
    }
}
