using System.Collections.Generic;
using UnityEngine;

public class UnitStatsBlock : PanelStat
{
    [SerializeField] private List<StatView> _statViews;

    public override void InitPanelStat(string name, UpgrateStatsView upgrateStatsView)
    {
        base.InitPanelStat(name, upgrateStatsView);

        foreach (StatView statView in _statViews)
            statView.InitPanelStat(name, this);
    }

    public WarriorData GetWarriorData(string name, int id)
    {
        return _upgrateStatsView.GetWarriorData(name, id);
    }

    public override void UpdateView(int id)
    {
        Debug.Log(id + " update");

        for (int i = 0; i < _statViews.Count; i++)
            _statViews[i].SetUnitId(id++);
    }

    public void UpdateStat(string name, int id, int idSkill)
    {
        _upgrateStatsView.UpdateStat(name, id, idSkill);
    }
}
