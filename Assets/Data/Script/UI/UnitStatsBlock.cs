using System.Collections.Generic;
using UnityEngine;

public class UnitStatsBlock : PanelStat
{
    [SerializeField] private List<StatView> _statViews;
    [SerializeField] protected int _bias;

    public override void InitPanelStat(UpgrateStatsView upgrateStatsView)
    {
        base.InitPanelStat(upgrateStatsView);

        foreach (StatView statView in _statViews)
            statView.InitPanelStat(_nameView ,this);
    }

    public WarriorData GetWarriorData(string name, int id)
    {
        return _upgrateStatsView.GetWarriorData(name, id);
    }

    public override void UpdateView(int id)
    {
        for (int i = 0; i < _statViews.Count; i++)
            _statViews[i].SetUnitId(id++ + _bias);
    }

    public void UpdateStat(string name, int id, int idSkill)
    {
        _upgrateStatsView.UpdateStat(name, id, idSkill);
    }
}
