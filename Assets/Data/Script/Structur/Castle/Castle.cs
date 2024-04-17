using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour, IStructur, IEntity
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _healPoint;
    private bool _isAlive = true;
    private bool _isDead = false;
    //private bool _isTarget = false;

    private Trash _trash;
    private Effect _effectDamage;
    private Effect _effectDestruct;

    private List<Barrack> _barracks;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;
    public bool IsAlive => _isAlive;

    public int Income { get; private set; }
    public int Money { get; private set; }
    public float MaxHealPoint { get; private set; }
    public Counter Counter { get; private set; }
    public Counter EnemyCounter { get; private set; }
    public string Name { get; private set; }
    public PointCreator PointCreator { get; private set; }
    public CashAccount CashAccount { get; private set; }

    public event UnityAction<float> HealPointChange;
    public event UnityAction<string> CastleDestroyed;

    public void Destruct()
    {
        _isAlive = false;
        Counter.DeleteEntity(this);
        CastleDestroyed?.Invoke(Name);
    }

    public void GetDamage(float damage, AttackType attackType)
    {
        _healPoint -= damage;

        HealPointChange?.Invoke(_healPoint);

        if (_healPoint <= 0 && _isDead == false)
        {
            _healPoint = 0;
            _isDead = true;
            StartCoroutine(DestructEffetc());
        }

        if (_effectDamage != null)
            _effectDamage.StartEffect();
    }

    public void InitializeCastle(DataGameInfo dataGameInfo, List<Barrack> structurs, BarracksData dataStructureBarracks, Trash trash, PointCreator pointCreator)
    {
        InitializeStruct(dataGameInfo.DataStructure);

        Counter = new Counter();
        Counter.AddEntity(this);
        Money = dataGameInfo.StartMoney;
        Name = dataGameInfo.Name;
        dataGameInfo.DataStructure.Name = Name;
        dataStructureBarracks.DataStructure.Name = Name;
        _barracks = structurs;
        _trash = trash;
        PointCreator = pointCreator;

        CashAccount = new CashAccount(Name, Income);

        for (int i = 0; i < structurs.Count; i++)
            structurs[i].InitializeBarracks(dataStructureBarracks, Counter, trash);
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        Income = dataStructure.Income;
        MaxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = MaxHealPoint;

        if (dataStructure.EffectDamage != null)
            _effectDamage = Instantiate(dataStructure.EffectDamage, transform);

        if (dataStructure.EffectDestruct != null)
            _effectDestruct = Instantiate(dataStructure.EffectDestruct, transform);
    }

    public void InitializeEvent()
    {
        HealPointChange?.Invoke(_healPoint);
    }

    public void SetEnemyCounter(Counter enemyCounter)
    {
        EnemyCounter = enemyCounter;

        foreach (var barrack in _barracks)
        {
            barrack.SetEnemyCounter(enemyCounter);
        }
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator DestructEffetc()
    {
        if (_effectDestruct != null)
        {
            Destruct();
            _spriteRenderer.enabled = false;
            _effectDestruct.StartEffect();

            yield return new WaitForSeconds(_effectDestruct.Duration);

            _trash.AddQueue(this);
        }
    }
}
