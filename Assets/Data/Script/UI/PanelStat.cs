using System.Collections.Generic;
using UnityEngine;

public class PanelStat : MonoBehaviour, IPanelStat
{
    [SerializeField] private List<int> _correctId;
    [SerializeField] protected string _nameView;
    [SerializeField] private TypeBlockView _blockViewType;

    protected UpgrateStatsView _upgrateStatsView;
    protected string _name;

    public TypeBlockView TypeBlockView => _blockViewType;

    public virtual void InitPanelStat(UpgrateStatsView upgrateStatsView)
    {
        _upgrateStatsView = upgrateStatsView;
        _name = _nameView;
    }

    public virtual void UpdateView(int id)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckCorrect(int id)
    {
        foreach (int value in _correctId)
        {
            if (value == id)
                return true;
        }

        return false;
    }
}
