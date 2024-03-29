using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class Barrack : MonoBehaviour, IStructur, IEntity
{
    [SerializeField] private int _startIdUnit;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _timeWaitDeath;

    private Transform _spawnPoint;
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;
    private bool _isAlive = true;

    private float _spawnTimeUnit;
    private float _waveTimeSpawn;
    private List<Wave> _waves;

    private Wave _currentWave = null;
    private float _nextTimeWave;
    private int _currentIdWave = 0;

    private bool _isWait = false;
    private bool _isSpawn = true;
    private bool _isEnd = false;
    private bool _isDead = false;

    private int _id = 0;//time

    private Counter _counter;
    private Counter _enemyCounter;
    private PointCreator _pointCreator;
    private Trash _trash;
    private Effect _effectDamage;
    private Effect _effectDestruct;

    private Warrior _unitPrefab;
    private Dictionary<TypeUnit, Unit> _unitsPrefab = new Dictionary<TypeUnit, Unit>();
    private Dictionary<VariertyUnit, WarriorData> _unitsStat = new Dictionary<VariertyUnit, WarriorData>();

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;
    public GameObject GameObject => gameObject;
    public Vector3 Position => transform.position;
    public int StartIdUnit => _startIdUnit;

    public bool IsAlive => _isAlive;

    public event UnityAction<Barrack> EndWave;
    public event UnityAction DestroyBarrack;
    public event UnityAction<float> HealthChange;

    public void Destruct()
    {
        _isAlive = false;
        DestroyBarrack?.Invoke();
        _counter.DeleteEntity(this);
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;
        _name = dataStructure.Name;

        _healPoint = _maxHealPoint;
        _spawnPoint = transform.GetChild(0);

        if (dataStructure.EffectDamage != null)
            _effectDamage = Instantiate(dataStructure.EffectDamage, transform);

        if (dataStructure.EffectDestruct != null)
            _effectDestruct = Instantiate(dataStructure.EffectDestruct, transform);
    }

    public void InitializeBarracks(BarracksData barracksData, Counter counter, Trash trash)
    {
        InitializeStruct(barracksData.DataStructure);

        _counter = counter;
        _pointCreator = barracksData.PointCreator;
        _trash = trash;
        _counter.AddEntity(this);

        _spawnTimeUnit = barracksData.SpawnerData.SpawnTime;
        _waveTimeSpawn = barracksData.SpawnerData.WaveTimeSpawn;
        _waves = barracksData.SpawnerData.Waves;
        _isWait = barracksData.IsWait;

        foreach (PrefabUnit prefab in barracksData.Prefabs.Prefabs)
            _unitsPrefab.Add(prefab.TypeUnit, prefab.Prefab);

        foreach (StatsPrefab StatsPrefab in barracksData.StatsPrefab)
            _unitsStat.Add(StatsPrefab.VariertyUnit, StatsPrefab.WarriorData);
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        _enemyCounter = enemyCounter;
    }

    public void GetDamage(float damage, AttackType attackType)
    {
        _healPoint -= damage;

        if (_healPoint <= 0 && _isDead == false)
        {
            _healPoint = 0;
            _isDead = true;
            StartCoroutine(DestructEffetc());
        }

        if (_effectDamage != null)
            _effectDamage.StartEffect();

        HealthChange?.Invoke(_healPoint);
    }

    public int GetIncome()
    {
        return _income;
    }

    public void SpawnUnits()
    {
        StartCoroutine(SetWave());
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ContinueSpawn()
    {
        StartCoroutine(SetWave());
    }

    public IEnumerator SetWave()
    {
        if (_currentWave != null)
            yield return new WaitForSeconds(_waveTimeSpawn);

        if (_currentIdWave < _waves.Count)
            _currentWave = _waves[_currentIdWave++];
        else
            _isEnd = true;

        if(!_isEnd)
            StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        if (_currentWave == null || _currentWave.SubWaves.Count == 0)
            yield break;

        SubWave subWave = null;
        Unit currentPrefab;
        WarriorData warriorData;
        Part currentPart;


        for (int id = 0; id < _currentWave.SubWaves.Count; id++)
        {
            if (subWave != null)
                yield return new WaitForSeconds(subWave.TimeNextSubWave);

            subWave = _currentWave.SubWaves[id];

            for (int i = 0; i < subWave.Parts.Count; i++)
            {
                currentPart = subWave.Parts[i];
                currentPrefab = null;
                warriorData = null;

                for (int j = 0; j < currentPart.Number; j++)
                {
                    if (currentPrefab == null)
                    {
                        currentPrefab = GetPrefabUnit(currentPart.TypeUnit);
                        warriorData = GetStatsUnit(currentPart.UnitVar);

                        if (currentPrefab == null || warriorData == null)
                        {
                            Debug.Log("�� �������");

                            break;
                        }
                    }

                    var unit = Instantiate(currentPrefab);
                    InitNewUnit(unit, warriorData);

                    if (j == currentPart.Number - 1 && i == subWave.Parts.Count - 1)
                        break;
                    else if ((currentPart.Number == 1 && subWave.Parts.Count == 1) || (j <= currentPart.Number - 1 && i <= subWave.Parts.Count - 1))
                        yield return new WaitForSeconds(_spawnTimeUnit);
                }
            }
        }

        EndWave?.Invoke(this);

        if (_isWait)
            _isSpawn = false;

        if (_isSpawn)
            ContinueSpawn();
    }

    private void InitNewUnit(Unit unit, WarriorData warriorData)
    {
        unit.Died += OnDied;
        _counter.AddEntity(unit);
        unit.LoadStats(warriorData);
        unit.InitUnit(_pointCreator.CreateRangePoint, _enemyCounter, _id++, Name);
        unit.ChangePosition(_spawnPoint.position);
        _id++;
    }

    private Unit GetPrefabUnit(TypeUnit type)
    {
        return _unitsPrefab.TryGetValue(type, out Unit unit) ? unit : null;
    }

    private WarriorData GetStatsUnit(VariertyUnit type)
    {
        return _unitsStat.TryGetValue(type, out WarriorData unit) ? unit : null;
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        _counter.DeleteEntity(unit);

        StartCoroutine(WaitTimeDeathEffect(unit));
    }

    private IEnumerator WaitTimeDeathEffect(Unit unit)
    {
        Debug.Log(_timeWaitDeath);

        yield return new WaitForSeconds(_timeWaitDeath);

        Debug.Log("Wait");

        _trash.AddQueue(unit);
    }

    private IEnumerator DestructEffetc()
    {
        Destruct();
        _spriteRenderer.enabled = false;

        if (_effectDestruct != null)
        {
            _effectDestruct.StartEffect();

            yield return new WaitForSeconds(_effectDestruct.Duration);

            _trash.AddQueue(this);
        }
    }
}
