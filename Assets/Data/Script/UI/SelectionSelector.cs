using System.Collections.Generic;
using UnityEngine;

public class SelectionSelector : MonoBehaviour
{
    [SerializeField] private List<AllocableObject> _allocableObjects;

    private UpgrateStatsView _statsView;

    private void OnDisable()
    {
        _statsView.ChangeCurrentId -= OnChangeCurrentId;
    }

    public void InitSelectionSelector(UpgrateStatsView upgrateStatsView)
    {
        _statsView = upgrateStatsView;
        upgrateStatsView.ChangeCurrentId += OnChangeCurrentId;
    }

    private void OnChangeCurrentId(int id)
    {
        foreach (var allocableObject in _allocableObjects)
        {
            if(allocableObject.GetUnitId == id)
                allocableObject.SetEnable();
            else
                allocableObject.SetDisable();
        }
    }
}
