using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Effects")]
    [SerializeField] private Effect _startEffect;
    [SerializeField] private float _timeStartEffect;

    [SerializeField] private Effect _updateEffect;

    [SerializeField] private Effect _endEffect;
    [SerializeField] private float _timeEndEffect;

    private float _damage;
    private bool _isDestroy = false;
    private float _timeDestroy;
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
        if (_updateEffect != null)
            _updateEffect.StopEffect();

        IEntity enemy = collision.GetComponent<IEntity>();

        if (enemy != null)
            enemy.GetDamage(_damage);

        Destroy();
    }

    public void PullOutOfGun()
    {
        _rigidbody.velocity = (_target - transform.position).normalized * _speed;

        if (_updateEffect != null)
            _updateEffect.StartEffect();
    }

    private void Destroy()
    {
        _isDestroy = true;
        _rigidbody.velocity = Vector3.zero;

        if(_endEffect != null)
            StartCoroutine(WaitEffect(_endEffect, _timeEndEffect));
    }

    private IEnumerator WaitEffect(Effect effect, float time)
    {
        effect.StartEffect();

        yield return new WaitForSeconds(time);

        effect.StopEffect();
    }
}
