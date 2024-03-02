using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Fireball : Spell
{
    [SerializeField] private float _damage;
    [SerializeField] private float _lifetimeDelay;
    [SerializeField] private float _lifetime;

    private List<Unit> _units;
    private bool _isInit;

    private void Awake()
    {
        _units = new List<Unit>();
        _isInit = true;
    }

    private void Update()
    {
        if (_lifetime > 0)
        {
            _lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        if (_isInit)
        {
            if (_lifetimeDelay > 0)
            {
                _lifetimeDelay -= Time.deltaTime;
            }
            else
            {
                _isInit = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Unit target))
        {
            Debug.Log("Gang");
            if (!_units.Contains(target) && target.Name != "Player")
            {
                _units.Add(target);
                target?.GetDamage(_damage);
            }
        }
    }
}
