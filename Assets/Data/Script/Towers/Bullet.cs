using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _damage;
    private Vector3 _target;
    private Rigidbody2D _rigidbody;

    public void Initialization(Vector3 target, float damage)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _damage = damage;
        _target = target;

        PullOutOfGun();
    }

    private void Update()
    {
        if (Mathf.Abs((_target - transform.position).magnitude) <= 0.05f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEntity enemy))
        {
            enemy.GetDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void PullOutOfGun()
    {
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;
    }
}
