using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IEntity
{
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _attackRange;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _maxHealPoint;

    private Counter _counter;
    private Trash _trash;
    private Warrior _target;
    private List<Warrior> _possibleTargets;

    private float _rechangeDelay = 0;
    private float _healPoint;

    public Vector3 Position => transform.position;

    public GameObject GameObject => gameObject;

    public int Income => throw new NotImplementedException();

    private void OnValidate()
    {
        if (_bulletPrefab != null && !_bulletPrefab.TryGetComponent(out Bullet newBullet))
        {
            _bulletPrefab = null;
        }
        GetComponent<CircleCollider2D>().radius = _attackRange;
    }

    public void Initialization(Counter counter, Trash trash)
    {
        _healPoint = _maxHealPoint;
        _counter = counter;
        _counter.AddEntity(this);
        _trash = trash;
        _possibleTargets = new List<Warrior>();
    }

    private void Update()
    {
        if (_target != null)
        {
            Shooting();
            if (Vector2.Distance(_target.transform.position, transform.position) > _attackRange)
            {
                _target = null;
            }
        }
        else
        {
            if (_possibleTargets.Count > 0)
            {
                FindTarget();
            }
        }

        if (_rechangeDelay > 0)
        {
            _rechangeDelay -= Time.deltaTime;
        }
        else
        {
            _rechangeDelay = 0;
        }
    }

    private void Shooting()
    {
        if (_rechangeDelay <= 0 && _target != null)
        {
            _rechangeDelay = _cooldown;
            Instantiate(_bulletPrefab, _muzzlePosition.position, Quaternion.identity).TryGetComponent(out Bullet newBullet);
            newBullet.Initialization(_target);
        }
    }

    private void FindTarget()
    {
        float minDistance = _attackRange;
        Warrior target = null;

        foreach (var warrior in _possibleTargets)
        {
            if (warrior != null)
            {
                float currentDistance = Vector2.Distance(warrior.transform.position, transform.position);
                if (currentDistance < minDistance)
                {
                    target = warrior;
                    minDistance = currentDistance;
                }
            }
        }

        _target = target;
        ClearList();
    }

    private void ClearList()
    {
        for (int i = 0; i < _possibleTargets.Count; i++)
        {
            Warrior warrior = _possibleTargets[i];

            if (warrior == null || Vector2.Distance(warrior.transform.position, transform.position) > _attackRange * 2)
            {
                _possibleTargets.RemoveAt(i);
                i--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Warrior enemy) && enemy.isEnemy)
        {
            _possibleTargets.Add(enemy);
        }
    }

    public void GetDamage(float damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
        {
            Destruct();
        }
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Destruct()
    {
        _counter.DeleteEntity(this);
        _trash.AddQueue(this);
    }
}
