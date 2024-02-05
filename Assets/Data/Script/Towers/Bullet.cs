using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private IEntity _target;
    private Vector3 _targetPosition;

    public void Initialization(IEntity target, float damage)
    {
        _damage = damage;
        _target = target;
        _targetPosition = _target.Position;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 6f) 
            {
                Destroy(gameObject);
            }
            else
            {
                _targetPosition = _target.Position;
            }
        }
        if (Vector3.Distance(transform.position, _targetPosition) < 0.05f)
        {
            _target.GetDamage(_damage);
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out IEntity enemy) && enemy == _target)
    //    {
    //        Debug.Log(enemy + " " + _damage);
    //        enemy.GetDamage(_damage);
    //        Destroy(gameObject);
    //    }
    //}
}
