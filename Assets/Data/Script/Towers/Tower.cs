using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour, IEntity, IStructur
{
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

    private Trash _trash;
    private CircleCollider2D _circleCollider;
    private Unit _currentTarget = null;

    private float _time = 0;

    public Vector3 Position => transform.position;

    public GameObject GameObject => gameObject;

    public int Income => _income;

    public bool IsAlive => _isAlive;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && unit.Name != _name)
        {
            if (_currentTarget == null)
            {
                _currentTarget = unit;
            }
            else
            {
                float currentDistance = Mathf.Abs((_currentTarget.Position - transform.position).magnitude);
                float newDistance = Mathf.Abs((unit.Position - transform.position).magnitude);

                if (newDistance < currentDistance)
                    _currentTarget = unit;
            }
        }
    }

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

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Destruct()
    {
        _isAlive = false;
        _trash.AddQueue(this);
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
        {
            _healPoint = 0;

            _trash.AddQueue(this);
        }
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void Initialization(TowerData towerData, Trash trash)
    {
        _trash = trash;

        _damage = towerData.Damage;
        _attackRange = towerData.AttackRange;
        _speedAttack = towerData.SpeedAttack;
        _prefabBullet = towerData.Bullet;
        _drawRadius = towerData.DrawRadius;

        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius = _attackRange;

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
