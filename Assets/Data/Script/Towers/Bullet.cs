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

    public void Initialization(IEntity target, float damage, float maxDistanceFly)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _damage = damage;
        _target = target;
        _maxDistanceFly = maxDistanceFly;
        _entityPosition = _target.Position;

        PullOutOfGun();
    }

    private void Update()
    {
        if (Mathf.Abs((_entityPosition - transform.position).magnitude) >= 1.5 * _maxDistanceFly)
            Destroy();

        //Debug.Log((_entityPosition - transform.position).magnitude);

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
        //}
    }
        //}
        _rigidbody.velocity = (_entityPosition - transform.position).normalized * _speed;

        if (_updateEffect != null)
            _updateEffect.StartEffect();
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
        //}
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;
        //}
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;
        //}
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;

        _rigidbody.velocity = (_target - transform.position).normalized * _speed;
    {
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;
    }
}
