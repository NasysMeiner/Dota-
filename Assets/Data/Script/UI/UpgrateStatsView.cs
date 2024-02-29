using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UpgrateStatsView : MonoBehaviour
{
    private List<Content> _contents;
    private UnitStatsBlock _prefabBlock;
    private Dictionary<string, List<UnitStatsBlock>> _blocks = new();
    private StatsContainer _container = new();

    private ChangerStats _changerStats;

    private int _currentContainer = 0;

    public void InitUpgrateStatsView(ChangerStats changerStats, List<Content> containers)
    {
        _contents = containers;
        _changerStats = changerStats;
        _changerStats.AddStat += AddUnitBlocks;
    }

    public void AddUnitBlocks(string name, int count)
    {
        if(_currentContainer < _contents.Count)
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

    public void LoadConteiner(List<Stat> stats)
    {
        //_container.Health.LoadStat(stats[0].Levels.) = stats[0];
    }
}
