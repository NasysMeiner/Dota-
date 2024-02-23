using UnityEngine;

public class Tower : MonoBehaviour, IEntity, IStructur
{
    [SerializeField] private TowerRadius _towerRadius;

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

    private Trash _trash;
    private Counter _counter;
    private Unit _currentTarget = null;

    private float _time = 0;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;
    
    public int Income => _income;
    public bool IsAlive => _isAlive;
    public string Name => _name;    
    public Unit CurrentTarget => _currentTarget;

    private void Update()
    {
        if (_currentTarget != null && _time >= _speedAttack)
        {
            if (Mathf.Abs((_currentTarget.Position - transform.position).magnitude) > _attackRange)
            {
                _currentTarget = null;
            }
            else
            {
                _time = 0;
                ShootTarget();
            } 
        }

        _time += Time.deltaTime;
    }

    public void ChangeTarget(Unit unit)
    {
        _currentTarget = unit;
        Debug.Log(_currentTarget);
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Destruct()
    {
        _isAlive = false;
        _currentTarget = null;
        _counter.DeleteEntity(this);
        _trash.AddQueue(this);
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0 && _isDead == false)
        {
            _healPoint = 0;
            _isDead = true;
            Destruct();
        }
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void Initialization(TowerData towerData, Trash trash, Counter counter)
    {
        _trash = trash;
        _counter = counter;

        _damage = towerData.Damage;
        _attackRange = towerData.AttackRange;
        _speedAttack = towerData.SpeedAttack;
        _prefabBullet = towerData.Bullet;
        _drawRadius = towerData.DrawRadius;

        _towerRadius.InitTowerRadius(this, _attackRange);

        InitializeStruct(towerData.DataStructure);
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;
        _healPoint = _maxHealPoint;
    }

    private void ShootTarget()
    {
        Bullet bullet = Instantiate(_prefabBullet);
        bullet.transform.position = transform.position;
        Vector3 targetPosition = CalculeutVector(bullet);
        bullet.Initialization(_currentTarget, targetPosition, _damage, _attackRange);
    }

    private Vector3 CalculeutVector(Bullet bullet)
    {
        Vector3 resultVector = _currentTarget.Position;

        float time = (_currentTarget.Position - transform.position).magnitude / bullet.Speed;

        float x = _currentTarget.Position.x + time * _currentTarget.MeshAgent.velocity.x;
        float y = _currentTarget.Position.y + time * _currentTarget.MeshAgent.velocity.x;

        resultVector = new Vector3(x, y, _currentTarget.Position.z);

        return resultVector;
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
