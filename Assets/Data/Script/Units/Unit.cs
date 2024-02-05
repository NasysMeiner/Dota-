using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    //||Bременно||
    public string CurrentState;
    public bool Pathboll;
    public bool isEnemy;
    public int _id;
    public string _name;
    public bool _target = false;
    public string Targettt;
    public int _pointId = 0;
    //||Временно||

    protected NavMeshAgent _meshAgent;
    protected StateMachine _stateMachine;
    protected Path _path;
    protected Scout _scout;
    protected SpriteRenderer _spriteRenderer;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public IEntity CurrentTarget { get; protected set; }
    public float Damage { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttckRange { get; protected set; }
    public float HealPoint { get; protected set; }
    public float VisibilityRange { get; protected set; }
    public float Speed { get; protected set; }
    public float ApproximationFactor { get; protected set; }

    public event UnityAction<Unit> Died;

    private void OnDisable()
    {
        UnitOnDisable();
    }

    private void Update()
    {
        UnitUpdate();
    }

    private void LateUpdate()
    {
        UnitLateUpdate();
    }

    public void ChangePosition(Vector3 position)
    {
        _meshAgent.Warp(position);
    }

    public void InitUnit(Path path, Counter counter, int id, string name)
    {
        _id = id;
        _name = name;


        _meshAgent = GetComponent<NavMeshAgent>();
        _meshAgent.speed = Speed;
        _path = path;

        _scout = new Scout(counter, transform, VisibilityRange);
        _scout.ChangeTarget += OnChangeTarget;

        _stateMachine = new StateMachine(this);

        CreateState();
    }

    public virtual void LoadStats(WarriorData warriorData)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material.color = warriorData.Color;
        HealPoint = warriorData.HealPoint;
        Damage = warriorData.AttackDamage;
        AttckRange = warriorData.AttackRange;
        VisibilityRange = warriorData.VisibilityRange;
        AttackSpeed = warriorData.AttackSpeed;
        Speed = warriorData.Speed;
        ApproximationFactor = warriorData.ApproximationFactor;
    }

    public virtual void GetDamage(float damage)
    {
        Debug.Log(damage);
        HealPoint -= damage;

        if (HealPoint <= 0)
            Die();
    }

    protected virtual void UnitOnDisable()
    {
        _scout.ChangeTarget -= OnChangeTarget;
        _scout.OnDisable();
    }

    protected virtual void UnitUpdate()
    {
        if (_stateMachine != null && _stateMachine.CurrentState != null)
        {
            _stateMachine.Update();
            CurrentState = _stateMachine.CurrentTextState;//временно
            Pathboll = _meshAgent.hasPath;
        }
    }

    protected virtual void CreateState()
    {
        _stateMachine.AddState(new AttackState(_stateMachine));
        _stateMachine.AddState(new WalkState(_stateMachine, _path, _meshAgent, SearchStartPointId()));
        _stateMachine.AddState(new IdleState(_stateMachine));

        _stateMachine.SetState<WalkState>();
    }

    protected virtual void UnitLateUpdate()
    {
        if (_scout != null)
            _scout.LateUpdate();
    }

    protected void Die()
    {
        _stateMachine.Stop();
        _meshAgent.enabled = false;
        Died?.Invoke(this);
    }

    protected int SearchStartPointId()
    {
        return Mathf.Abs((transform.position - _path.StandartPath[0].transform.position).magnitude) < Mathf.Abs((transform.position - _path.StandartPath[_path.StandartPath.Count - 1].transform.position).magnitude) ? 0 : _path.StandartPath.Count - 1;
    }

    private void OnChangeTarget(IEntity entity)
    {
        CurrentTarget = entity;

        if (entity != null)
            _target = true;
        else
            _target = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Position, VisibilityRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, AttckRange);
    }
}
