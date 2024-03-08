using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthBarUpdater))]
public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    //Временно
    [SerializeField] private GameObject _HealthBar;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;

    [SerializeField] private bool _isDrawRadius;

    //||B�������||
    public string CurrentState;
    public bool Pathboll;
    public int _id;
    public string _name;
    public bool _target = false;
    public string Targettt;
    public int _pointId = 0;
    public bool isDie = false;
    //||��������||

    private bool _isAlive = true;
    protected NavMeshAgent _meshAgent;
    protected Rigidbody2D _rigidbody;
    protected StateMachine _stateMachine;
    protected Vector3 _targetPoint;
    protected Scout _scout;
    protected Quaternion _startRotation;

    protected List<Skill> _skillList = new();

    protected Effect _effectAttack;
    private Effect _effectDamage;
    private Effect _effectDeath;
    private float _timeEffectDeath;

    private HealthBarUpdater _healthBarUpdater;
    private AnimateChanger _animateChanger;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;
    public NavMeshAgent MeshAgent => _meshAgent;
    public bool IsAlive => _isAlive;

    public IEntity CurrentTarget { get; protected set; }
    public float Damage { get; protected set; }
    public string Name { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttckRange { get; protected set; }
    public float MaxHealthPoint { get; private set; }
    public float HealPoint { get; protected set; }
    public float VisibilityRange { get; protected set; }
    public float Speed { get; protected set; }
    public float ApproximationFactor { get; protected set; }
    public Counter EnemyCounter { get; protected set; }

    public event UnityAction<Unit> Died;

    //
    public HealthBar healthBar;
    public event UnityAction<float> HealthChanged;
    //

    private void OnDisable()
    {
        UnitOnDisable();
    }

    private void Update()
    {
        if (_isAlive)
            UnitUpdate();
    }

    private void LateUpdate()
    {
        if (_isAlive)
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

        _animateChanger = new AnimateChanger();
        _animateChanger.Init(_animator);

        CreateState();
    }

    public virtual void LoadStats(WarriorData warriorData)
    {
        _healthBarUpdater = GetComponent<HealthBarUpdater>();

        if (warriorData == null)
            throw new System.NotImplementedException("Stats Null");

        if (warriorData.Avatar != null)
        {
            _animator.runtimeAnimatorController = warriorData.Avatar;
            _animator.transform.localScale = Vector3.one * 0.25f; //Temporarily, there are no animations yet
        }
        else
        {
            if (warriorData.Sprite != null)
                _spriteRenderer.sprite = warriorData.Sprite;
            else
                throw new System.NotImplementedException("Sprite Null");
            _animator.transform.localScale = Vector3.one;//Temporarily, there are no animations yet
        }

        HealPoint = warriorData.HealPoint;
        MaxHealthPoint = HealPoint;
        Damage = warriorData.AttackDamage;
        AttckRange = warriorData.AttackRange;
        VisibilityRange = warriorData.VisibilityRange;
        AttackSpeed = warriorData.AttackSpeed;
        Speed = warriorData.Speed;
        ApproximationFactor = warriorData.ApproximationFactor;

        foreach(Skill skill in warriorData.SkillList)
        {
            Skill newSkill = Instantiate(skill, transform);
            newSkill.SetUnit(this);
            _skillList.Add(newSkill);

            if (newSkill.TypeSkill == TypeSkill.InitStart || newSkill.TypeSkill == TypeSkill.StatsUp)
                newSkill.UseSkill();
        }

        if (warriorData.EffectDamage != null)
            _effectDamage = Instantiate(warriorData.EffectDamage, transform);

        if (warriorData.EffectAttack != null)
            _effectAttack = Instantiate(warriorData.EffectAttack, transform);

        if (warriorData.EffectDeath != null)
            _effectDeath = Instantiate(warriorData.EffectDeath, transform);

        _timeEffectDeath = warriorData.TimeEffectDeath;

        _healthBarUpdater.InitHealthBar(this);
    }

    public virtual void GetDamage(float damage)
    {
        HealPoint -= damage;

        if (HealPoint <= 0 && isDie == false)
        {
            isDie = true;
            Die();
            CurrentTarget = null;
        }

        if (_effectDamage != null)
            _effectDamage.StartEffect();

        HealthChanged?.Invoke(HealPoint);

        UpdateHealthBar();
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
            CurrentState = _stateMachine.CurrentTextState;
            Pathboll = _meshAgent.hasPath;

            if (_meshAgent.velocity.x != 0)
                _spriteRenderer.flipX = !(_meshAgent.velocity.x < 0);

            if (_meshAgent.velocity != Vector3.zero || transform.rotation.z != 0)
                transform.rotation = _startRotation;
        }
    }

    protected virtual void CreateState()
    {
        State state = new AttackState(_stateMachine, _effectAttack);
        state.onEnter += _animateChanger.OnPlayHit;
        _stateMachine.AddState(state);

        state = new WalkState(_stateMachine, _targetPoint, _meshAgent);
        state.onEnter += _animateChanger.OnPlayWalk;
        _stateMachine.AddState(state);

        state = new IdleState(_stateMachine);
        state.onEnter += _animateChanger.OnPlayDamage;
        _stateMachine.AddState(state);

        _stateMachine.SetState<WalkState>();
    }

    protected virtual void UnitLateUpdate()
    {
        _scout?.LateUpdate();
    }

    protected void Die()
    {
        _isAlive = false;
        _stateMachine.Stop();

        if(_skillList.Count > 0)
            foreach(Skill skill in _skillList)
                if (skill.TypeSkill == TypeSkill.Deatch)
                    skill.UseSkill();

        _spriteRenderer.enabled = false;
        _HealthBar.SetActive(false);

        if (_effectDeath != null)
            _effectDeath.StartEffect();

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
        if (_isDrawRadius)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Position, VisibilityRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, AttckRange);
        }
    }
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(HealPoint);
        }
    }
}
