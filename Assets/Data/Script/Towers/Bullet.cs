using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Effect")]
    [SerializeField] private Effect _startEffect;
    [SerializeField] private float _timeStartEffect;
    [SerializeField] private Effect _updateEffect;
    [SerializeField] private Effect _endEffect;
    [SerializeField] private float _timeEndEffect;

    private float _damage;
    private bool _isDestroy = false;
    private float _timeDestroy;
    private IEntity _target;
    private Vector3 _entityPosition;
    private float _maxDistanceFly;
    private Rigidbody2D _rigidbody;

    public float Speed => _speed;

    public void Initialization(IEntity target, Vector3 targetPosition, float damage, float maxDistanceFly)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _damage = damage;
        _target = target;
        _maxDistanceFly = maxDistanceFly;
        _entityPosition = targetPosition;

        PullOutOfGun();
    }

    private void Update()
    {
        if (Mathf.Abs((_entityPosition - transform.position).magnitude) >= 1.5 * _maxDistanceFly)
            Destroy();

        if (_isDestroy)
        {
            _timeDestroy += Time.deltaTime;

            if (_timeDestroy >= _timeEndEffect + 0.5f)
                Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEntity enemy) && enemy == _target)
        {
            _updateEffect?.StopEffect();

            enemy?.GetDamage(_damage);

            Destroy();
        }
    }

    public void PullOutOfGun()
    {
        _rigidbody.velocity = (_entityPosition - transform.position).normalized * _speed;

        _updateEffect?.StartEffect();
    }

    private void Destroy()
    {
        _isDestroy = true;
        _rigidbody.velocity = Vector3.zero;

        if (_endEffect != null)
            StartCoroutine(WaitEffect(_endEffect, _timeEndEffect));
    }

    private IEnumerator WaitEffect(Effect effect, float time)
    {
        effect.StartEffect();

        yield return new WaitForSeconds(time);

        effect.StopEffect();
    }
}
