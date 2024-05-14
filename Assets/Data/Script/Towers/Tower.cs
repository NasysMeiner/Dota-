using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour, IEntity, IStructur
{
    [SerializeField] private TowerRadius _towerRadius;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AllocableObject _allocableObject;
    [SerializeField] private Barrack _barrack;

    private bool _drawRadius;

    private Bullet _prefabBullet;
    private float _damage;
    private float _attackRange;
    private float _speedAttack;
    private float _maxHealPoint;
    private int _income;
    private string _name;

    private float _healPoint;
    private bool _isAlive = true;
    private bool _isDead = false;
    private bool _isShoot = false;

    private Trash _trash;
    private Counter _counter;
    private Unit _currentTarget = null;
    private UpgrateStatsView _upgrateStatsView;

    private Effect _effectDamage;
    private Effect _effectDestruct;
    private Effect _effectStart;
    private float _timeStartEffect;
    private ViewSkillPay _skillPay;

    private float _time = 0;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public int Income => _income;
    public bool IsAlive => _isAlive;
    public string Name => _name;
    public Unit CurrentTarget => _currentTarget;

    public float MaxHealPoint => _maxHealPoint;

    public event UnityAction<float> HealPointChange;
    public event UnityAction Died;

    private void Update()
    {
        if (_currentTarget != null && _time >= _speedAttack && _isShoot == false)
        {
            if (Mathf.Abs((_currentTarget.Position - transform.position).magnitude) > _attackRange || _currentTarget.IsAlive == false)
            {
                _currentTarget = null;
            }
            else
            {
                _isShoot = true;
                StartCoroutine(ShootTarget());
            }
        }

        _time += Time.deltaTime;
    }

    public void ChangeTarget(Unit unit)
    {
        _currentTarget = unit;
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Destruct()
    {
        Died?.Invoke();

        _upgrateStatsView.DestructTower(_barrack.StartIdUnit);

        if(_skillPay != null)
            _skillPay.Destruct();

        if (_allocableObject != null)
            _allocableObject.SetOffSelection();

        _isAlive = false;
        _currentTarget = null;
        _spriteRenderer.enabled = false;
        _isDead = true;
        _healPoint = 0;

        StartCoroutine(DestructEffetc());
    }

    public void Resurrect()
    {
        if (_skillPay != null)
            _skillPay.Return();

        if (_allocableObject != null)
            _allocableObject.SetOnSelection();

        _isAlive = true;
        _spriteRenderer.enabled = true;
        _isDead = false;
        _healPoint = _maxHealPoint;
    }

    public void GetDamage(float damage, AttackType attackType)
    {
        _healPoint -= damage;

        if (_healPoint <= 0 && _isDead == false)
            Destruct();

        if (_effectDamage != null)
            _effectDamage.StartEffect();

        HealPointChange?.Invoke(_healPoint);
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void Initialization(TowerData towerData, Trash trash, Counter counter, UpgrateStatsView upgrateStatsView)
    {
        _trash = trash;
        _counter = counter;
        _upgrateStatsView = upgrateStatsView;

        _damage = towerData.Damage;
        _attackRange = towerData.AttackRange;
        _speedAttack = towerData.SpeedAttack;
        _prefabBullet = towerData.Bullet;
        _drawRadius = towerData.DrawRadius;

        if (Name == "Player")
            _skillPay = GetComponent<ViewSkillPay>();

        _towerRadius.InitTowerRadius(this, _attackRange);

        InitializeStruct(towerData.DataStructure);
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;
        _healPoint = _maxHealPoint;
        _timeStartEffect = dataStructure.TimeStartEffect;

        if (dataStructure.EffectDamage != null)
            _effectDamage = Instantiate(dataStructure.EffectDamage, transform);

        if (dataStructure.EffectDestruct != null)
            _effectDestruct = Instantiate(dataStructure.EffectDestruct, transform);

        if (dataStructure.EffectStart != null)
            _effectStart = Instantiate(dataStructure.EffectStart, transform);
    }

    private IEnumerator ShootTarget()
    {
        if (_effectStart != null)
        {
            _effectStart.StartEffect();

            yield return new WaitForSeconds(_timeStartEffect);
        }

        Bullet bullet = Instantiate(_prefabBullet);
        bullet.transform.position = transform.position;
        Vector3 targetPosition = CalculeutVector(bullet);
        bullet.Initialization(_currentTarget, targetPosition, _damage, _attackRange, _name);
        _isShoot = false;
        _time = 0;
    }

    private Vector3 CalculeutVector(Bullet bullet)
    {
        if (bullet == null || _currentTarget == null || transform == null)
            return Vector3.zero;

        float time = (_currentTarget.Position - transform.position).magnitude / bullet.Speed;

        float x = _currentTarget.Position.x + time * _currentTarget.MeshAgent.velocity.x;
        float y = _currentTarget.Position.y + time * _currentTarget.MeshAgent.velocity.x;

        Vector3 resultVector = new(x, y, _currentTarget.Position.z);

        return resultVector;
    }

    private IEnumerator DestructEffetc()
    {
        if (_effectDestruct != null)
        {
            _effectDestruct.StartEffect();

            yield return new WaitForSeconds(_effectDestruct.Duration);
        }
    }

    private void OnDrawGizmos()
    {
        if (_drawRadius)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, _attackRange);
        }
    }
}
