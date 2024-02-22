using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RadiusSpawner : MonoBehaviour
{
    //[SerializeField] private List<Unit> _units = new List<Unit>();
    [SerializeField] private Unit _prefabUnit;
    [SerializeField] private WarriorData _stats1;

    [SerializeField] private float _cooldownSpawn = 2f;
    [SerializeField] private float _radius = 15.3f;

    private Dictionary<TypeUnit, Unit> _prefabs = new Dictionary<TypeUnit, Unit>();
    private List<DataUnitStats> _stats;

    private List<Radius> _radiusList;
    private List<Castle> _castleList;

    private List<Unit> _playerUnit = new List<Unit>();
    private List<Unit> _enemyUnit = new List<Unit>();

    private Trash _trash;

    private int _currentCastleSpawn = -1;
    private int _currentUnitId = -1;

    private bool _isReady = true;
    private float _time = 0;

    private void Update()
    {
        if (_isReady == false && _time >= _cooldownSpawn)
            _isReady = true;

        _time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnClick();
    }

    public void Init(List<Radius> radiusList, List<Castle> castleList, List<PrefabUnit> prefabs, List<DataUnitStats> stats, Trash trash)
    {
        foreach (PrefabUnit unit in prefabs)
            _prefabs.Add(unit.TypeUnit, unit.Prefab);

        _stats = stats;
        _trash = trash;
        _radiusList = radiusList;
        _castleList = castleList;
    }

    private void OnClick()
    {
        if (_isReady)
        {
            RaycastHit hit;
            Ray myRay;

            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(myRay, out hit, 100))
            {
                if (_currentUnitId >= 0 && hit.collider.TryGetComponent(out Ground ground) && CheckInSpawn(hit.point))
                {
                    _isReady = false;
                    WarriorData stat = null;
                    Unit newUnit = null;
                    Castle currentCastle = _castleList[_currentCastleSpawn];

                    if(_stats[_currentCastleSpawn].StatsPrefab.Count > _currentUnitId)
                        stat = _stats[_currentCastleSpawn].StatsPrefab[_currentUnitId].WarriorData;
                    else
                        stat = _stats[_currentCastleSpawn].StatsPrefab[_stats[_currentCastleSpawn].StatsPrefab.Count - 1].WarriorData;

                    if (_prefabs.TryGetValue(stat.Type, out Unit prefab))
                        newUnit = Instantiate(prefab);

                    if(_currentCastleSpawn == 0)
                        _playerUnit.Add(newUnit);
                    else
                        _enemyUnit.Add(newUnit);
                     

                    currentCastle.Counter.AddEntity(newUnit);
                    newUnit.Died += OnDied;
                    newUnit.LoadStats(stat);
                    newUnit.InitUnit(currentCastle.PointCreator.CreateRangePoint, currentCastle.EnemyCounter, 100, "Click");
                    newUnit.ChangePosition(hit.point);
                    //Debug.Log(hit.point + " Spawn");
                }
                else
                {
                    //Debug.Log("Nononono");
                }
            }

            _time = 0;
        }
    }

    public void ChangeActiveUnit(int id)
    {
        _currentUnitId = id;
    }

    private bool CheckInSpawn(Vector3 point)
    {
        for(int i = 0; i < _radiusList.Count; i++)
        {
            if (_radiusList[i].CheckInRadius(point))
            {
                _currentCastleSpawn = i;

                return true;
            }
        }

        return false;
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;

        if(unit.EnemyCounter == _castleList[0].EnemyCounter)
            _castleList[0].Counter.DeleteEntity(unit);
        else
            _castleList[1].Counter.DeleteEntity(unit);

        _trash.AddQueue(unit);
    }
}