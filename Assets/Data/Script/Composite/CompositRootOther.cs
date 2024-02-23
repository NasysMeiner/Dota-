using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositRootOther : CompositeRoot
{
    [SerializeField] private RadiusSpawner _radiusSpawner;
    [SerializeField] private List<Radius> _radiusList;
    [SerializeField] private List<Castle> _castleList;
    [SerializeField] private DataUnitPrefab _prefabUnit;
    [SerializeField] private List<DataUnitStats> _dataStats;
    [SerializeField] private Trash _trash;

    public override void Compose()
    {
        Init();
    }

    private void Init()
    {
        _radiusSpawner.Init(_radiusList, _castleList, _prefabUnit.Prefabs, _dataStats, _trash);
    }
}
