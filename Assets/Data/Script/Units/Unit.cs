using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
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
    public bool isDie = false;
    //||Временно||

    private bool _isAlive = true;
    protected NavMeshAgent _meshAgent;
    protected Rigidbody2D _rigidbody;
    protected StateMachine _stateMachine;
    protected Vector3 _targetPoint;
    protected Scout _scout;
    protected SpriteRenderer _spriteRenderer;
    protected Quaternion _startRotation;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;
    public NavMeshAgent MeshAgent => _meshAgent;
    public bool IsAlive => _isAlive;

    public IEntity CurrentTarget { get; protected set; }
    public float Damage { get; protected set; }
    public string Name { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttckRange { get; protected set; }
    public float HealPoint { get; protected set; }
    public float VisibilityRange { get; protected set; }
    public float Speed { get; protected set; }
    public float ApproximationFactor { get; protected set; }
    public Counter EnemyCounter { get; protected set; }

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

    public void InitUnit(Vector3 targetPoint, Counter counter, int id, string name)
    {
        _id = id;
        _name = name;
        Name = name;

        EnemyCounter = counter;
        _rigidbody = GetComponent<Rigidbody2D>();
        _meshAgent = GetComponent<NavMeshAgent>();
        _meshAgent.speed = Speed;
        _targetPoint = targetPoint;
        _startRotation = transform.rotation;

        _scout = new Scout(counter, transform, VisibilityRange);
        _scout.ChangeTarget += OnChangeTarget;
        _stateMachine = new StateMachine(this);

        CreateState();
    }

    public virtual void LoadStats(WarriorData warriorData)
    {
        if(warriorData == null)
            throw new System.NotImplementedException("Stats Null");

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (warriorData.Sprite != null)
            _spriteRenderer.sprite = warriorData.Sprite;
        else
            throw new System.NotImplementedException("Sprite Null");

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
        HealPoint -= damage;

        if (HealPoint <= 0)
        {
            Die();
            isDie = true;
        }
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

            if (_meshAgent.velocity != Vector3.zero || transform.rotation.z != 0)
                transform.rotation = _startRotation;
        }
    }

    protected virtual void CreateState()
    {
        _stateMachine.AddState(new AttackState(_stateMachine));
        _stateMachine.AddState(new WalkState(_stateMachine, _targetPoint, _meshAgent));
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
        _isAlive = false;
        _stateMachine.Stop();
        Died?.Invoke(this);
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
