using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerStorage : MonoBehaviour
{
    [SerializeField] private float _timeWait = 5f;
    [SerializeField] private int _startPriceRepair = 30;

    private Dictionary<string, List<TowerSlot>> _towers = new();
    private Dictionary<string, Counter> _counters = new();
    private Dictionary<string, int> _prices = new();
    private Bank _bank;

    public int Price => _startPriceRepair;

    public event UnityAction<string, int, Vector3> TowerDestruct;

    private void OnDisable()
    {
        foreach (var item in _towers)
            foreach (TowerSlot towerSlot in item.Value)
                towerSlot.OnDisable();
    }

    public void InitTowerStorage(List<Tower> allTowers, Bank bank, List<Castle> castles)
    {
        _bank = bank;


        foreach (Castle castle in castles)
            _counters.Add(castle.Name, castle.Counter);

        foreach (var item in allTowers)
        {
            if (_towers.ContainsKey(item.Name))
            {
                _towers[item.Name].Add(new TowerSlot(item, this));
            }
            else
            {
                _towers.Add(item.Name, new List<TowerSlot> { new(item, this) });
                _prices.Add(item.Name, _startPriceRepair);
            }
        }
    }

    public int GetPrice(string name)
    {
        return _prices[name];
    }

    public void AddTowerCounter(string name, Tower tower)
    {
        Counter counter = _counters[name];
        counter.AddEntity(tower);
    }

    public void DeleteTowerCounter(string name, Tower tower)
    {
        Counter counter = _counters[name];
        counter.DeleteEntity(tower);
    }

    public void StartTimer(TowerSlot towerSlot)
    {
        List<TowerSlot> slots = _towers[towerSlot.Name];
        int id = -1;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == towerSlot)
            {
                id = i;

                break;
            }
        }

        Debug.Log(id);

        StartCoroutine(TimerRepair(towerSlot.Name, id, towerSlot.Position));
    }

    public bool RepairTower(string name, int id)
    {
        if (_bank.Pay(GetPrice(name), name) == false)
            return false;

        TowerSlot towerSlot = _towers[name][id];
        towerSlot.RepairTower();

        return true;
    }

    public IEnumerator TimerRepair(string name, int id, Vector3 position)
    {
        yield return new WaitForSeconds(_timeWait);

        TowerDestruct?.Invoke(name, id, position);
    }
}