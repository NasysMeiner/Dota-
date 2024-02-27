using System.Collections;
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

    [Header("Bank")]
    [SerializeField] private Bank _bank;
    [SerializeField] private float _timeAddMoney = 3f;

    public override void Compose()
    {
        InitTowers();
        InitBank();
    }

    private void InitTowers()
    {
        _radiusSpawner.Init(_radiusList, _castleList, _prefabUnit.Prefabs, _dataStats, _bank, _trash);

        foreach(Tower item in _towerListPlayer)
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
}
