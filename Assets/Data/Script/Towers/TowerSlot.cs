using Unity.VisualScripting;
using UnityEngine;

public class TowerSlot
{
    private TowerStorage _towerStorage;

    private Tower _tower;

    protected Vector3 _startPosition;

    public string Name => _tower.Name;
    public Vector3 Position => _tower.Position;
    public Vector3 StartPosition => _startPosition;

    public TowerSlot(Tower tower, TowerStorage towerStorage)
    {
        _tower = tower;
        _towerStorage = towerStorage;

        _towerStorage.AddTowerCounter(_tower.Name, _tower);
        _startPosition = tower.Position;

        _tower.Died += OnDied;
    }

    public void OnDisable()
    {
        _tower.Died -= OnDied;
    }

    public void RepairTower()
    {
        _tower.Resurrect();
        _tower.ChangePosition(_startPosition);
        _towerStorage.AddTowerCounter(Name, _tower);
    }

    public void ChangePosition(Vector3 position)
    {
        _tower.transform.position = position;
    }

    private void OnDied()
    {
        _towerStorage.DeleteTowerCounter(_tower.Name, _tower);
        _towerStorage.StartTimer(this);
    }
}
