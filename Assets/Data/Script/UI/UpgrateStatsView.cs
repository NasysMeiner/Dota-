using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgrateStatsView : MonoBehaviour
{
    private List<Content> _contents;
    private UnitStatsBlock _prefabBlock;
    private Dictionary<string, List<UnitStatsBlock>> _blocks = new();
    private readonly StatsContainer _container = new();

    private ChangerStats _changerStats;

    private int _currentContainer = 0;

    private void OnDisable()
    {
        _changerStats.ChangeUnitStat -= UpdateView;
    }

    public void InitUpgrateStatsView(ChangerStats changerStats, List<Content> containers, UnitStatsBlock prefabBlock)
    {
        _contents = containers;
        _changerStats = changerStats;
        _prefabBlock = prefabBlock;
        _changerStats.ChangeUnitStat += UpdateView;

        foreach (var user in _changerStats.Users)
            AddUnitBlocks(user.Key, user.Value.Count);

        UpdateView();
    }

    public void AddUnitBlocks(string name, int count)
    {
        if (_currentContainer < _contents.Count)
        {
            List<UnitStatsBlock> unitStatsBlocks = new();

            for (int i = 0; i < count; i++)
            {
                UnitStatsBlock unitStats = Instantiate(_prefabBlock, _contents[_currentContainer].transform);
                unitStats.InitUnitStatsView(i, name, this);
                unitStats.UpdateValuesStats(_container);
                unitStatsBlocks.Add(unitStats);
            }

            _blocks.Add(name, unitStatsBlocks);
            _currentContainer++;
        }
    }

    public void UpdateView()
    {
        foreach (var block in _blocks)
        {
            for (int i = 0; i < block.Value.Count; i++)
            {
                LoadConteiner(block.Key, i);
                block.Value[i].UpdateValuesStats(_container);
            }
        }
    }

    public void IncreaseLevel(string name, int idStat, int id)
    {
        _changerStats.IncreaseLevel(name, idStat, id);
    }

    private void LoadConteiner(string name, int id)
    {
        ContainerPack containerPack = _changerStats.GetStatUnit(name, id);

        List<CurrentStat> currentStats = containerPack.CurrentStats;
        List<Stat> stats = containerPack.Stats;
        List<PriceStat> prices = containerPack.Prices;

        int currentLevelStat;
        int nextLevelStat;
        int priceStat;

        _container.Name = containerPack.Name;

        for (int i = 0; i < stats.Count; i++)
        {
            int currentLevelNumber = currentStats[i].CurrentLevel - 1;
            int nextLevelNumber;

            if (currentLevelNumber < stats[i].Levels.Count)
                currentLevelStat = stats[i].Levels[currentLevelNumber];
            else
                throw new NotImplementedException("Level very high");

            if (currentLevelNumber + 1 < stats[i].Levels.Count)
            {
                nextLevelStat = stats[i].Levels[currentLevelNumber + 1];
                nextLevelNumber = currentLevelNumber + 1;
            }
            else
            {
                nextLevelStat = currentLevelStat;
                nextLevelNumber = currentLevelNumber;
            }

            if (currentLevelNumber != nextLevelNumber)
            {
                if (currentLevelNumber + 1 < prices[i].Price.Count)
                    priceStat = prices[i].Price[currentLevelNumber + 1];
                else
                    priceStat = prices[i].Price[prices[i].Price.Count - 1];
            }
            else
            {
                priceStat = -1;
            }

            _container.LoadStat(currentLevelStat, nextLevelStat, priceStat, i);
        }
    }
}
