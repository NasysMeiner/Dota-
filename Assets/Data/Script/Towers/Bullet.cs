using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Effect")]
    [SerializeField] private Effect _updateEffect;
    [SerializeField] private Effect _endEffect;
    [SerializeField] private float _timeEndEffect;

    protected float _damage;
    protected string _name;
    private bool _isDestroy = false;
    private float _timeDestroy;
    private IEntity _target;
    private Vector3 _entityPosition;
    private float _maxDistanceFly;
    private Rigidbody2D _rigidbody;

    public float Speed => _speed;

    public virtual void Initialization(IEntity target, Vector3 targetPosition, float damage, float maxDistanceFly, string name)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _damage = damage;
        _target = target;
        _name = name;
        _maxDistanceFly = maxDistanceFly;
        _entityPosition = targetPosition;
    }

    private void Update()
    {
        if (Mathf.Abs((_entityPosition - transform.position).magnitude) >= 1.5 * _maxDistanceFly || _target.IsAlive == false)
            Destroy();

        if (_target != null)
            CheckTargetPosition();

        if (_isDestroy)
        {
            _timeDestroy += Time.deltaTime;

            if (_timeDestroy >= _timeEndEffect)
                Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, _entityPosition, Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEntity enemy) && enemy == _target)
        {
            if (_updateEffect != null)
                _updateEffect.StopEffect();

            MakeDamage(enemy);
            Destroy();
        }
    }

    protected virtual void MakeDamage(IEntity enemy)
    {
        enemy?.GetDamage(_damage);
    }

    private void Destroy()
    {
        if (_endEffect != null && _isDestroy == false)
            StartCoroutine(WaitEffect(_endEffect, _timeEndEffect));

        _isDestroy = true;
        _rigidbody.velocity = Vector3.zero;
    }

    private void CheckTargetPosition()
    {
        if (_target.IsAlive)
            _entityPosition = _target.Position;
    }

    private IEnumerator WaitEffect(Effect effect, float time)
    {
        effect.StartEffect();

        yield return new WaitForSeconds(time);

        effect.StopEffect();
    }
}
