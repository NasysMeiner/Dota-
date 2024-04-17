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

    private void OnChangeCurrentId(int id, TypeBlockView typeBlockView = TypeBlockView.MainType)
    {
        if(typeBlockView == TypeBlockView.MainType)
            MainSelection(id);
        else if(typeBlockView == TypeBlockView.AdditionalType)
            AdditionalSelection(id);
    }

    private void MainSelection(int id)
    {
        foreach (var allocableObject in _allocableObjects)
        {
            if (allocableObject.GetUnitId == id)
                allocableObject.SetEnable();
            else
                allocableObject.SetDisable();
        }
    }

    private void AdditionalSelection(int id)
    {
        foreach (var allocableObject in _allocableObjects)
        {
            if (allocableObject.GetUnitId == id)
                allocableObject.SetEnable();
            else if(allocableObject.TypeBlockView == TypeBlockView.AdditionalType)
                allocableObject.SetDisable();
        }
    }
}
