using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgrateStatsView : MonoBehaviour
{
    [SerializeField] private List<PanelStat> _panelStats;

    private PanelStat _activePanel;

    private ChangerStats _changerStats;

    private int _currentId;

    public void InitUpgrateStatsView(ChangerStats changerStats)
    {
        _changerStats = changerStats;
    }

    public WarriorData GetWarriorData(string name, int id)
    {
        return _changerStats.GetWarriorData(name, id);
    }

    public void OnChangeId(int id)
    {
        _currentId = id;

        foreach(PanelStat stat in _panelStats)
        {
            if (stat.CheckCorrect(_currentId))
            {
                stat.UpdateView(_currentId);
                _activePanel = stat;

                return;
            }
        }
    }

    public void OnChangeUnitStat()
    {
        _activePanel.UpdateView(_currentId);
    }
}
