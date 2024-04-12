using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairView : MonoBehaviour
{
    private TowerStorage _towerStorage;
    private RepairTowerButton _prefab;

    private List<RepairTowerButton> _buttons = new();

    private void OnDisable()
    {
        if(_towerStorage != null)
            _towerStorage.TowerDestruct -= OnTowerDestruct;
    }

    public void InitRepairView(TowerStorage towerStorage, RepairTowerButton prefab)
    {
        _towerStorage = towerStorage;
        _prefab = prefab;

        _towerStorage.TowerDestruct += OnTowerDestruct;
    }

    public bool TryRepairTower(string name, int id)
    {
        return _towerStorage.RepairTower(name, id);
    }

    private void OnTowerDestruct(string name, int id, Vector3 position)
    {
        RepairTowerButton button = GetInActiveButton();
        button.PlaceButton(name, id, _towerStorage.GetPrice(name), position);
    }

    private RepairTowerButton GetInActiveButton()
    {
        foreach(var button in _buttons)
            if(button.IsActive == false)
                return button;

        CreateRepairButton();
        return GetInActiveButton();
    }

    private void CreateRepairButton()
    {
        var newButton = Instantiate(_prefab);
        newButton.InitButton(this);
        _buttons.Add(newButton);
    }
}
