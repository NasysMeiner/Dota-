using Unity.VisualScripting;
using UnityEngine;

public class TowerSlot
{
    private TowerStorage _towerStorage;

    private Tower _tower;

    public string Name => _tower.Name;
    public Vector3 Position => _tower.Position;

    public TowerSlot(Tower tower, TowerStorage towerStorage)
    {
        _tower = tower;
        _towerStorage = towerStorage;

        _towerStorage.AddTowerCounter(_tower.Name, _tower);

        _tower.Died += OnDied;
    }

    public void OnDisable()
    {
        _tower.Died -= OnDied;
    }

    public void RepairTower()
    {
        _tower.Resurrect();
        _towerStorage.AddTowerCounter(Name, _tower);
    }

    private void OnDied()
    {
        _towerStorage.DeleteTowerCounter(_tower.Name, _tower);
        _towerStorage.StartTimer(this);
    }
}
