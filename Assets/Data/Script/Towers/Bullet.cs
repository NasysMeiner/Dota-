using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Warrior _target;
    private Vector3 _targetPosition;

    public void Initialization(Warrior target)
    {
        _target = target;
        _targetPosition = _target.transform.position;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (_target._healPoints > 0) /////???????
            {
                _targetPosition = _target.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Warrior enemy) && enemy == _target)
        {
            enemy.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
