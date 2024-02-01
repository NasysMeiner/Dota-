using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class Warrior : Unit
{
    [SerializeField] private Path _path;

    public string CurrentState;//временно
    public bool Pathboll;

    private NavMeshAgent _meshAgent;
    private StateMachine _stateMachine;
    private Scout _scout;

    public float _healPoints = 10;
    public int _id;
    public string _name;
    public bool _target = false;
    public string Targettt;
    public int _pointId = 0;
    private float _visibilityRange = 3f;
    private float _attackRange = 1f;
    private float _attackSpeed = 1f;
    private float _damage = 6f;
    private float _minDistancePoint = 0.4f;

    public IEntity CurrentTarget { get; private set; }
    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float AttckRange => _attackRange;
    public float HealPoint => _healPoints;
    public float MinDistancePoint => _minDistancePoint;

    public override event UnityAction<Warrior> Died;

    private void OnDisable()
    {
        _scout.ChangeTarget -= OnChangeTarget;
        _scout.OnDisable();
    }

    private void Update()
    {
        if (_stateMachine != null && _stateMachine.CurrentState != null)
        {
            _stateMachine.Update();
            CurrentState = _stateMachine.CurrentTextState;//временно
            Pathboll = _meshAgent.hasPath;
        }
    }

    private void LateUpdate()
    {
        if(_scout != null)
            _scout.LateUpdate();
    }

    public override void InitUnit(Path path, Counter counter, int id, string name )
    {
        _id = id;
        _name = name;


        _meshAgent = GetComponent<NavMeshAgent>();
        _path = path;

        _scout = new Scout(counter, transform, _visibilityRange);
        _scout.ChangeTarget += OnChangeTarget;
        
        int startPointId = Math.Abs((transform.position - _path.StandartPath[0].transform.position).magnitude) < Math.Abs((transform.position - _path.StandartPath[_path.StandartPath.Count - 1].transform.position).magnitude) ? 0 : _path.StandartPath.Count - 1;

        _stateMachine = new StateMachine(this);
        _stateMachine.AddState(new AttackState(_stateMachine));
        _stateMachine.AddState(new WalkState(_stateMachine, _path, _meshAgent, startPointId));
        _stateMachine.AddState(new IdleState(_stateMachine));

        _stateMachine.SetState<WalkState>();
    }

    public override void GetDamage(float damage)
    {
        _healPoints -= damage;

        if (_healPoints <= 0)
            Die();
    }

    public override void ChangePosition(Vector3 position)
    {
        _meshAgent.Warp(position);
    }

    private void Die()
    {
        _stateMachine.Stop();
        _meshAgent.enabled = false;
        Died?.Invoke(this);
    }

    private void OnChangeTarget(IEntity entity)
    {
        CurrentTarget = entity;

        if (entity != null)
            _target = true;
        else
            _target = false;
    }
}
