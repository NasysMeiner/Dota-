using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadiusSpawner : MonoBehaviour
{
    [SerializeField] private float _cooldownSpawn = 2f;
    [SerializeField] private float _radius = 7f;

    [Space]
    [SerializeField] private float _timeWaitDeath;

    private Dictionary<TypeUnit, Unit> _prefabs = new();
    private List<DataUnitStats> _stats;

    private List<Radius> _radiusList;
    private List<Castle> _castleList;

    private List<Unit> _playerUnit = new();
    private List<Unit> _enemyUnit = new();

    private Trash _trash;
    private Bank _bank;

    private int _currentCastleSpawn = -1;
    private int _currentUnitId = -1;

    private bool _isReady = true;
    private float _time = 0;

    public event UnityAction<int> ChangeId;

    private void Update()
    {
        if (_isReady == false && _time >= _cooldownSpawn)
            _isReady = true;

        _time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnClick();
    }

    public void Init(List<Radius> radiusList, List<Castle> castleList, List<PrefabUnit> prefabs, List<DataUnitStats> stats, Bank bank, Trash trash)
    {
        foreach (PrefabUnit unit in prefabs)
            _prefabs.Add(unit.TypeUnit, unit.Prefab);

        _stats = stats;
        _trash = trash;
        _bank = bank;
        _radiusList = radiusList;
        _castleList = castleList;

        foreach (Radius radius in _radiusList)
            radius.InitRadius(_radius);
    }

    public void ChangeActiveUnit(int id)
    {
        _currentUnitId = id;
    }

    public int GetPriceUnit(int id)
    {
        return _stats[0].StatsPrefab[id].WarriorData.Price;
    }

    public string GetNameUnit(int id)
    {
        return _stats[0].StatsPrefab[id].WarriorData.Type.ToString();
    }

    private void OnClick()
    {
        if (_isReady)
        {
            Ray myRay;
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out RaycastHit hit, 100))
            {
                if(hit.collider.TryGetComponent(out RepairTowerButton towerButton))
                {
                    towerButton.Repair();

                    return;
                }

                if (hit.collider.TryGetComponent(out BarrackBoxId barrack))
                {
                    ChangeId?.Invoke(barrack.Barrack.StartIdUnit);
                }
                else if (_currentUnitId >= 0 && hit.collider.TryGetComponent(out Ground ground) && CheckInSpawn(hit.point))
                {
                    Spawn(hit);
                }
                else if (_currentUnitId == -1 && !hit.collider.TryGetComponent(out UiBlock block))
                {
                    ChangeId?.Invoke(_currentUnitId);
                }
                else
                {
                    //Debug.Log("Nononono");
                }
            }
        }
    }

    private void Spawn(RaycastHit hit)
    {
        _isReady = false;
        WarriorData stat = null;
        Unit newUnit = null;
        Castle currentCastle = _castleList[_currentCastleSpawn];

        if (_stats[_currentCastleSpawn].StatsPrefab.Count > _currentUnitId)
            stat = _stats[_currentCastleSpawn].StatsPrefab[_currentUnitId].WarriorData;
        else
            stat = _stats[_currentCastleSpawn].StatsPrefab[_stats[_currentCastleSpawn].StatsPrefab.Count - 1].WarriorData;

        if (_prefabs.TryGetValue(stat.Type, out Unit prefab) && _bank.Pay(stat.Price, currentCastle.Name))
        {
            newUnit = Instantiate(prefab);

            if (_currentCastleSpawn == 0)
                _playerUnit.Add(newUnit);
            else
                _enemyUnit.Add(newUnit);

            currentCastle.Counter.AddEntity(newUnit);
            _trash.WriteUnit(newUnit);
            newUnit.LoadStats(stat);
            newUnit.InitUnit(currentCastle.PointCreator.CreateRangePoint, currentCastle.EnemyCounter, 100, currentCastle.Name);
            newUnit.ChangePosition(hit.point);

            _time = 0;
        }
        else
        {
            //Debug.Log("No money");
        }
    }

    private bool CheckInSpawn(Vector3 point)
    {
        for (int i = 0; i < _radiusList.Count; i++)
        {
            if (_radiusList[i].CheckInRadius(point))
            {
                _currentCastleSpawn = i;

                return true;
            }
        }

        return false;
    }
}