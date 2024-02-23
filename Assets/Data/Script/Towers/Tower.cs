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

        if (_possibleTargets.Count > 0)
        {
            _target = GetClosestTarget();

            if (_target != null)
            {
                Shooting();
            }
        }
        Debug.Log("Список возможных целей:");
        foreach (var enemy in _possibleTargets)
        {
            Debug.Log(enemy.name);
        }

    }
    private void Shooting()
    {
        if (_rechangeDelay <= 0 && _target != null)
        {
            _rechangeDelay = _cooldown;
            Instantiate(_bulletPrefab, _muzzlePosition.position, Quaternion.identity).TryGetComponent(out Bullet newBullet);
            newBullet.Initialization(_target, 10f, _attackRange);
        }
    }
    private Warrior GetClosestTarget()
    {
        Warrior closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (var warrior in _possibleTargets)
        {
            if (warrior != null)
            {
                float distance = Vector2.Distance(warrior.transform.position, transform.position);
                if (distance < minDistance)
                {
                    closestTarget = warrior;
                    minDistance = distance;
                }
            }
        }

        return closestTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Warrior enemy = collision.GetComponent<Warrior>();
        if (enemy != null && enemy.isEnemy)
        {
            _possibleTargets.Add(enemy);
            Debug.Log("Добавлен враг ");
        }
        else
        {
            Debug.Log("Не удалось добавить врага " + collision.name + ". Объект не является врагом.");
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
