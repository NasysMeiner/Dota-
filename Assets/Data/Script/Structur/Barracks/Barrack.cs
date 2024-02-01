using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barrack : MonoBehaviour, IStructur, IEntity
{
    public bool isEnemy;

    private Transform _spawnPoint;
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;

    private float _spawnTimeUnit;
    private float _waveTimeSpawn;
    private List<Wave> _waves;

    private Wave _currentWave = null;
    private float _nextTimeWave;
    private int _currentIdWave = 0;

    private bool _isWait = false;
    private bool _isSpawn = true;

    private int _id = 0;//time

    private Path _path;
    private Counter _counter;
    private Counter _enemyCounter;
    private Trash _trash;

    private Warrior _unitPrefab;
    private Dictionary<TypeUnit, Unit> _unitsPrefab = new Dictionary<TypeUnit, Unit>();

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;
    public GameObject GameObject => gameObject;
    public Vector3 Position => transform.position;

    public event UnityAction<Barrack> EndWave;
    public event UnityAction DestroyBarrack;

    public void Destruct()
    {
        DestroyBarrack?.Invoke();
        _counter.DeleteEntity(this);
        _trash.AddQueue(this);
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;
        _name = dataStructure.Name;

        _healPoint = _maxHealPoint;
        _spawnPoint = transform.GetChild(0);
    }

    public void InitializeBarracks(BarracksData barracksData, Path path, Counter counter, Trash trash)
    {
        InitializeStruct(barracksData.DataStructure);

        _path = path;
        _counter = counter;
        _trash = trash;
        _counter.AddEntity(this);

        _spawnTimeUnit = barracksData.SpawnerData.SpawnTime;
        _waveTimeSpawn = barracksData.SpawnerData.WaveTimeSpawn;
        _waves = barracksData.SpawnerData.Waves;
        _isWait = barracksData.IsWait;

        foreach (PrefabUnit prefab in barracksData.Prefabs)
        {
            _unitsPrefab.Add(prefab.TypeUnit, prefab.Prefab);
        }
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        _enemyCounter = enemyCounter;
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
            Destruct();
    }

    public int GetIncome()
    {
        return _income;
    }

    public void SpawnUnits()
    {
        //int unit = 20;
        //float timeSpawn = 1.5f;

        //StartCoroutine(Spawn(timeSpawn, unit));

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

        Debug.Log(_currentIdWave);
        if (_currentIdWave < _waves.Count)
            _currentWave = _waves[_currentIdWave++];

        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        if (_currentWave == null || _currentWave.SubWaves.Count == 0)
            yield break;

        SubWave subWave = null;
        Unit currentPrefab;
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
                //Debug.Log(currentPart.Number);

                for (int j = 0; j < currentPart.Number; j++)
                {
                    //if(j != 0 || currentPart.Number == 1)
                    //    yield return new WaitForSeconds(_spawnTimeUnit);

                    if (currentPrefab == null)
                    {
                        currentPrefab = GetPrefabUnit(currentPart.TypeUnit);

                        if (currentPrefab == null)
                            break;
                    }

                    Unit unit = Instantiate(currentPrefab);
                    InitNewUnit(unit);

                    if(((currentPart.Number == 1 || j != currentPart.Number - 1) && (subWave.Parts.Count == 1 || i != subWave.Parts.Count - 1)) || (j == currentPart.Number - 1 && i != subWave.Parts.Count - 1))
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

    private void InitNewUnit(Unit unit)
    {
        unit.Died += OnDied;
        _counter.AddEntity(unit);
        unit.InitUnit(_path, _enemyCounter, _id++, Name);
        unit.ChangePosition(_spawnPoint.position);
        unit.isEnemy = isEnemy;
        _id++;
    }

    private Unit GetPrefabUnit(TypeUnit type)
    {
        return _unitsPrefab.TryGetValue(type, out Unit unit) ? unit : null;
    }

    //private IEnumerator Spawn(float time, int units)
    //{
    //    int id = 0;

    //    while (true)
    //    {
    //        if (units <= 0)
    //            break;

    //        Warrior newUnit = Instantiate(_unitPrefab);
    //        newUnit.Died += OnDied;
    //        newUnit.SetPosition(_spawnPoint.position);
    //        _counter.AddEntity(newUnit);
    //        newUnit.InitUnit(_path, _enemyCounter, id++, Name);
    //        newUnit.isEnemy = isEnemy;
    //        units--;

    //        yield return new WaitForSeconds(time);
    //    }
    //}

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;
        _counter.DeleteEntity(unit);
        _trash.AddQueue(unit);
    }
}
