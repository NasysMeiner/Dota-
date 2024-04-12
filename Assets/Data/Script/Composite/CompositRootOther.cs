using System.Collections.Generic;
using UnityEngine;

public class CompositRootOther : CompositeRoot
{
    [Header("Spawn In Click")]
    [SerializeField] private RadiusSpawner _radiusSpawner;
    [SerializeField] private List<Radius> _radiusList;
    [SerializeField] private List<Castle> _castleList;
    [SerializeField] private DataUnitPrefab _prefabUnit;
    [SerializeField] private List<DataUnitStats> _dataStats;
    [SerializeField] private Trash _trash;

    [Header("Tower Player")]
    [SerializeField] private List<Tower> _towerListPlayer;
    [SerializeField] private TowerData _towerDataPlayer;

    [Header("Tower AI")]
    [SerializeField] private List<Tower> _towerListAI;
    [SerializeField] private TowerData _towerDataAi;
    [SerializeField] private AutoSpawner _autoSpawner;
    [SerializeField] private SelectorPointSpawner _selectorPointSpawner;

    [Header("Bank")]
    [SerializeField] private Bank _bank;
    [SerializeField] private float _timeAddMoney = 3f;

    [Header("Tower")]
    [SerializeField] private RepairTowerButton _prefab;
    [SerializeField] private TowerStorage _towerStorage;
    [SerializeField] private RepairView _repairView;

    public override void Compose()
    {
        InitTowers();
        InitBank();
        InitAiSpawner();
        InitTowerStorage();
    }

    private void InitTowers()
    {
        _radiusSpawner.Init(_radiusList, _castleList, _prefabUnit.Prefabs, _dataStats, _bank, _trash);

        foreach (Tower item in _towerListPlayer)
            item.Initialization(_towerDataPlayer, _trash, _castleList[0].Counter);

        foreach (Tower item in _towerListAI)
            item.Initialization(_towerDataAi, _trash, _castleList[1].Counter);
    }

    private void InitBank()
    {
        _bank.InitBank(_timeAddMoney);

        foreach (Castle castle in _castleList)
            _bank.AddCashAccount(castle.CashAccount);
    }

    private void InitAiSpawner()
    {
        _autoSpawner.InitAutoSpawner(_dataStats[1], _castleList[1], _prefabUnit, _trash, _selectorPointSpawner);
    }

    private void InitTowerStorage()
    {
        List<Tower> towers = new();

        foreach(Tower tower in _towerListPlayer)
            towers.Add(tower);

        foreach(Tower tower in _towerListAI)
            towers.Add(tower);

        _towerStorage.InitTowerStorage(towers, _bank, _castleList);

        _repairView.InitRepairView(_towerStorage, _prefab);
    }
}
