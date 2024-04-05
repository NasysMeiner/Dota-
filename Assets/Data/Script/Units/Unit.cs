using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Scripting;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthBarUpdater))]
public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    //Временно
    [SerializeField] private GameObject _HealthBar;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private DamageColorEffectUnit _damageColorEffectUnit;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected GameObject _shadow;

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
    private bool _isDodgeRangeAttack = false;
    protected bool _isDoubleAttack = false;
    protected NavMeshAgent _meshAgent;
    protected Rigidbody2D _rigidbody;
    protected StateMachine _stateMachine;
    protected Vector3 _targetPoint;
    protected Scout _scout;
    protected Quaternion _startRotation;

    private float _timeImmortality = 0f;

    protected List<Skill> _skillList = new();

    protected Effect _effectAttack;
    private Effect _effectDamage;
    private Effect _effectDeath;

    private HealthBarUpdater _healthBarUpdater;
    protected AnimateChanger _animateChanger;

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
    public float ProjectileBlockChance { get; protected set; }
    public Counter EnemyCounter { get; protected set; }
    public bool IsDoubleAttack { get; protected set; }

    public event UnityAction<Unit> Died;

    //
    public HealthBar healthBar;
    public event UnityAction<float, AttackType> HealthChanged;
    //

    private void OnDisable()
    {
        UnitOnDisable();
    }

    private void Update()
    {
        if (_isAlive)
            UnitUpdate();

        if(_spriteRenderer != null)
            _spriteRenderer.sortingOrder = (int)(10000 - transform.position.y * 1000);
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
        _meshAgent.updateRotation = false;
        _meshAgent.angularSpeed = 0;
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
        _meshAgent = GetComponent<NavMeshAgent>();
        _healthBarUpdater = GetComponent<HealthBarUpdater>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (warriorData == null)
            throw new System.NotImplementedException("Stats Null");

        if (warriorData.Avatar != null)
        {
            _animator.runtimeAnimatorController = warriorData.Avatar;
            _animator.transform.localScale = Vector3.one * warriorData.Scale; //Temporarily, there are no animations yet
            _animator.transform.localPosition = warriorData.Bias;
        }
        else
        {
            if (warriorData.Sprite != null)
                _spriteRenderer.sprite = warriorData.Sprite;
            else
                throw new System.NotImplementedException("Sprite Null");
            _animator.transform.localScale = Vector3.one;//Temporarily, there are no animations yet
        }

        _shadow.transform.localPosition = warriorData.BiasShadow;
        _HealthBar.transform.localPosition = warriorData.BiasHPBar;

        HealPoint = warriorData.HealPoint;
        MaxHealthPoint = HealPoint;
        Damage = warriorData.AttackDamage;
        AttckRange = warriorData.AttackRange;
        VisibilityRange = warriorData.VisibilityRange;
        AttackSpeed = warriorData.AttackSpeed;
        Speed = warriorData.Speed;
        ApproximationFactor = warriorData.ApproximationFactor;
        ProjectileBlockChance = warriorData.ProjectileBlockChance;
        _timeImmortality = warriorData.TimeImmortaly;
        IsDoubleAttack = warriorData.IsDoubleAttack;
        _isDoubleAttack = warriorData.IsDoubleAttack;

        foreach(SkillCont skillCont in warriorData.SkillConts)
        {
            Debug.Log(skillCont.Skill + " " + skillCont.IsUnlock);
            if(skillCont != null && skillCont.IsUnlock && skillCont.Skill.TypeSkill != TypeSkill.StatsUp)
            {
                Skill newSkill;

                if(skillCont.Skill as SkillData)
                {
                    SkillData skillData = skillCont.Skill as SkillData;
                    newSkill = Instantiate(skillData.PrefabSkill, transform);
                    newSkill.SetUnit(this);
                    skillData.LoadData(newSkill);
                    _skillList.Add(newSkill);

                    if (newSkill.TypeSkill == TypeSkill.InitStart && skillCont.IsUnlock)
                    {
                        newSkill.UseSkill();
                    }
                }
            }
        }

        _damageColorEffectUnit.InitEffectDamage(this, warriorData.ColorEffectDamage);

        if (warriorData.EffectDamage != null)
            _effectDamage = Instantiate(warriorData.EffectDamage, transform);

        if (warriorData.EffectAttack != null)
            _effectAttack = Instantiate(warriorData.EffectAttack, transform);

        if (warriorData.EffectDeath != null)
            _effectDeath = Instantiate(warriorData.EffectDeath, transform);

        _healthBarUpdater.InitHealthBar(this);
    }

    public virtual void GetDamage(float damage, AttackType attackType)
    {
        if (ProjectileBlockChance > 0 && attackType == AttackType.Ranged)
        {
            int randomNumber = Random.Range(1, 100);

            if (randomNumber <= ProjectileBlockChance)
                _isDodgeRangeAttack = true;
        }

        if (_isDodgeRangeAttack == false)
        {
            HealPoint -= damage;

            if (HealPoint <= 0 && isDie == false)
            {
                isDie = true;
                StartCoroutine(LiveAfterDeath());
            }

            if (_effectDamage != null && attackType != AttackType.ConstDamage)
                _effectDamage.StartEffect();

            HealthChanged?.Invoke(HealPoint, attackType);

            UpdateHealthBar();
        }
        else
        {
            Debug.Log("Dodge");
            _isDodgeRangeAttack = false;
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
            CurrentState = _stateMachine.CurrentTextState;
            Pathboll = _meshAgent.hasPath;

            if (_meshAgent.velocity.x != 0)
                _spriteRenderer.flipX = !(_meshAgent.velocity.x < 0);
        }
    }

    protected virtual void CreateState()
    {
        State state = new AttackState(_stateMachine, _effectAttack, _isDoubleAttack);
        state.StateActive += _animateChanger.OnPlayHit;
        _stateMachine.AddState(state);

        state = new WalkState(_stateMachine, _targetPoint, _meshAgent);
        state.StateActive += _animateChanger.OnPlayWalk;
        _stateMachine.AddState(state);

        state = new IdleState(_stateMachine);
        state.StateActive += _animateChanger.OnPlayIdle;
        _stateMachine.AddState(state);

        state = new DeathState(_stateMachine);
        state.StateActive += _animateChanger.OnPlayDeath;
        _stateMachine.AddState(state);

        _stateMachine.SetState<WalkState>();
    }

    protected virtual void UnitLateUpdate()
    {
        _scout?.LateUpdate();
    }

    protected virtual void Die()
    {
        _isAlive = false;
        _stateMachine.Die();

        if (_skillList.Count > 0)
            foreach (Skill skill in _skillList)
                if (skill.TypeSkill == TypeSkill.Deatch)
                    skill.UseSkill();
                else if (skill.TypeSkill == TypeSkill.InitStart)
                    skill.StopSkill();

        //_spriteRenderer.enabled = false;
        _HealthBar.SetActive(false);

        if (_effectDeath != null)
            _effectDeath.StartEffect();

        Died?.Invoke(this);
    }

    private IEnumerator LiveAfterDeath()
    {
        yield return new WaitForSeconds(_timeImmortality);

        Die();
        CurrentTarget = null;
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
