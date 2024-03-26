using System.Collections.Generic;
using UnityEngine;

public class UpgrateStatsView : MonoBehaviour
{
    [SerializeField] private PanelStat _defaultPanel;
    [SerializeField] private List<PanelStat> _panelStats;

    private PanelStat _activePanel;

    private ChangerStats _changerStats;
    private RadiusSpawner _radiusSpawner;

    private int _currentId;

    private void OnDisable()
    {
        _radiusSpawner.ChangeId -= OnChangeId;
        _changerStats.ChangeUnitStat -= OnChangeUnitStat;
    }

    public void InitUpgrateStatsView(ChangerStats changerStats, RadiusSpawner radiusSpawner)
    {
        _changerStats = changerStats;
        _radiusSpawner = radiusSpawner;

        _radiusSpawner.ChangeId += OnChangeId;
        _changerStats.ChangeUnitStat += OnChangeUnitStat;
        _activePanel = _defaultPanel;
        _currentId = -1;

        foreach(PanelStat panelStat in _panelStats)
        {
            panelStat.InitPanelStat("Player", this);
        }
    }

    public WarriorData GetWarriorData(string name, int id)
    {
        return _changerStats.GetWarriorData(name, id);
    }

    public void OnChangeId(int id)
    {
        foreach (PanelStat stat in _panelStats)
        {
            if (stat.CheckCorrect(id))
            {
                PanelStat nextPanel;

                if (id == _currentId)
                {
                    _activePanel.gameObject.SetActive(false);
                    _activePanel = _defaultPanel;
                    _activePanel.gameObject.SetActive(true);
                    _currentId = -1;

                    return;
                }
                else
                {
                    _currentId = id;
                    nextPanel = stat;
                    stat.UpdateView(_currentId);
                }

                _activePanel.gameObject.SetActive(false);
                nextPanel.gameObject.SetActive(true);
                _activePanel = nextPanel;

                return;
            }
        }
    }

    public void OnChangeUnitStat()
    {
        _activePanel.UpdateView(_currentId);
    }

    public void UpdateStat(string name, int id, int idSkill)
    {
        if (idSkill >= 0)
            _changerStats.UnlockSkill(name, id, idSkill);
        else
            _changerStats.IncreaseLevelUnit(name, id);
    }
}
