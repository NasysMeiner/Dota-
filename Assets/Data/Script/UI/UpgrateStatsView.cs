using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgrateStatsView : MonoBehaviour
{
    private List<Content> _contents;
    private UnitStatsBlock _prefabBlock;
    private Dictionary<string, List<UnitStatsBlock>> _blocks = new();
    private StatsContainer _container = new();

    private ChangerStats _changerStats;

    private int _currentContainer = 0;

    public void InitUpgrateStatsView(ChangerStats changerStats, List<Content> containers, UnitStatsBlock prefabBlock)
    {
        _contents = containers;
        _changerStats = changerStats;
        _prefabBlock = prefabBlock;
        _changerStats.AddStat += AddUnitBlocks;
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
                _blocks.Add(name, unitStatsBlocks);
            }

            _currentContainer++;
        }
    }

    public void UpdateView()
    {
        foreach(var block in _blocks)
        {
            for(int i = 0; i < block.Value.Count; i++)
            {
                LoadConteiner(block.Key, i);
                block.Value[i].UpdateValuesStats(_container);
            }
        }
    }

    private void LoadConteiner(string name, int id)
    {
        ContainerPack containerPack = _changerStats.GetStatUnit(name, id);

        List<CurrentStat> currentStats = containerPack.CurrentStats;
        List<Stat> stats = containerPack.Stats;
        List<PriceStat> prices = containerPack.Prices;

        int currentLevelNumber = 0;

        int currentLevelStat = 0;
        int nextLevelStat = 0;
        int priceStat = 0;

        for (int i = 0; i < stats.Count; i++)
        {
            currentLevelNumber = currentStats[i].CurrentLevel - 1;

            if (currentLevelNumber < stats[i].Levels.Count)
                currentLevelStat = stats[i].Levels[currentLevelNumber];
            else
                throw new NotImplementedException("Level very high");

            if (currentLevelNumber + 1 < stats[i].Levels.Count)
                nextLevelStat = stats[i].Levels[currentLevelStat + 1];
            else
                nextLevelStat = currentLevelStat;

            if(currentLevelStat != nextLevelStat)
            {
                if(currentLevelStat + 1 < prices[i].Price.Count)
                    priceStat = prices[i].Price[currentLevelStat + 1];
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
