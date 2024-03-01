using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitStatsBlock : MonoBehaviour
{
    [SerializeField] private List<TypeStat> _types = new();
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private StatView _healthPoint;
    [SerializeField] private StatView _attack;
    [SerializeField] private StatView _attackSpeed;

    private int _id;
    private string _name;
    private UpgrateStatsView _statsView;

    public void InitUnitStatsView(int id, string name, UpgrateStatsView upgrateStatsView)
    {
        _id = id;
        _name = name;
        _statsView = upgrateStatsView;
    }

    public void UpdateValuesStats(StatsContainer statsContainer)
    {
        _nameText.text = statsContainer.Name;
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
        _statsView.IncreaseLevel(_name, typeStat, _id);
    }
}

[System.Serializable]
public class StatsContainer
{
    public string Name;
    public StatContainer Health { get; private set; }
    public StatContainer Attack { get; private set; }
    public StatContainer AttackSpeed { get; private set; }

    public StatsContainer()
    {
        Health = new StatContainer();
        Attack = new StatContainer();
        AttackSpeed = new StatContainer();
    }

    public void LoadStat(int currentLevel, int nextLevel, int price, int id)
    {
        if(id == 0)
            Health.LoadStat(currentLevel, nextLevel, price);
        else if(id == 1)
            Attack.LoadStat(currentLevel, nextLevel, price);
        else if(id == 2)
            AttackSpeed.LoadStat(currentLevel, nextLevel, price);
    }
}

[System.Serializable]
public class StatContainer
{
    public int CurrentLevel { get; private set; }
    public int NextLevel { get; private set; }
    public int Price { get; private set; }

    public void LoadStat(int currentLevel, int nextLevel, int price)
    {
        CurrentLevel = currentLevel;
        NextLevel = nextLevel;
        Price = price;
    }
}
