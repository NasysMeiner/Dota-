using System.Collections.Generic;
using UnityEngine;

public class PanelStat : MonoBehaviour, IPanelStat
{
    [SerializeField] private List<int> _correctId;

    protected UpgrateStatsView _upgrateStatsView;
    protected string _name;

    public virtual void InitPanelStat(string name, UpgrateStatsView upgrateStatsView)
    {
        _upgrateStatsView = upgrateStatsView;
        _name = name;
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
