using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgrateStatsView : MonoBehaviour
{
    [SerializeField] private PanelStat _defaultPanel;
    [SerializeField] private List<PanelStat> _panelStats;

    private PanelStat _activePanel;

    private ChangerStats _changerStats;
    private RadiusSpawner _radiusSpawner;

    private int _currentId;
    private int _currentAdditionalId = -1;

    private List<PanelStat> _additionalPanel = new();
    private List<PanelStat> _mainPanel = new();

    public event UnityAction<int, TypeBlockView> ChangeCurrentId;

    private void OnDisable()
    {
        _radiusSpawner.ChangeId -= OnChangeId;
        _changerStats.ChangeUnitStat -= OnChangeUnitStat;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            EnableDefaultPanel();
    }

    public void InitUpgrateStatsView(ChangerStats changerStats, RadiusSpawner radiusSpawner)
    {
        _changerStats = changerStats;
        _radiusSpawner = radiusSpawner;

        _radiusSpawner.ChangeId += OnChangeId;
        _changerStats.ChangeUnitStat += OnChangeUnitStat;
        _activePanel = _defaultPanel;
        ChangeId(-1);

        foreach (PanelStat panelStat in _panelStats)
        {
            panelStat.InitPanelStat(this);

            if(panelStat.TypeBlockView == TypeBlockView.MainType)
                _mainPanel.Add(panelStat);
            else if(panelStat.TypeBlockView == TypeBlockView.AdditionalType)
                _additionalPanel.Add(panelStat);
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
                if (stat.TypeBlockView == TypeBlockView.MainType)
                    EnableMainPanel(id, stat);
                else if(stat.TypeBlockView == TypeBlockView.AdditionalType)
                    EnableAdditionalPanel(id, stat);

                return;
            }
        }
    }

    public void ChangeId(int id, TypeBlockView typeBlockView = TypeBlockView.MainType)
    {
        if(typeBlockView == TypeBlockView.MainType)
            _currentId = id;
        else if(typeBlockView == TypeBlockView.AdditionalType)
            _currentAdditionalId = id;

        ChangeCurrentId?.Invoke(id, typeBlockView);
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

    private void EnableAdditionalPanel(int id, PanelStat stat)
    {
        foreach (PanelStat panel in _additionalPanel)
            if (panel != stat)
                panel.gameObject.SetActive(false);

        if (_currentAdditionalId == id)
            id = -1;

        ChangeId(id, TypeBlockView.AdditionalType);

        if (id == -1)
        {
            if(stat != null)
                stat.gameObject.SetActive(false);

            return;
        }

        stat.UpdateView(id);
        stat.gameObject.SetActive(true);
    } 

    private void EnableMainPanel(int id, PanelStat stat)
    {
        if (id == _currentId)
        {
            EnableDefaultPanel();

            return;
        }

        ChangeId(id);
        stat.UpdateView(_currentId);
        _activePanel.gameObject.SetActive(false);
        stat.gameObject.SetActive(true);
        _activePanel = stat;
        EnableAdditionalPanel(-1, null);

        return;
    }

    private void EnableDefaultPanel()
    {
        if(_activePanel != _defaultPanel)
        {
            _activePanel.gameObject.SetActive(false);
            _activePanel = _defaultPanel;
            _activePanel.gameObject.SetActive(true);
        }

        EnableAdditionalPanel(-1, null);
        ChangeId(-1);
    }
}

[System.Serializable]
public enum TypeBlockView
{
    MainType,
    AdditionalType
}