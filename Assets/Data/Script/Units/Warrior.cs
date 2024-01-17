using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Warrior : Unit
{
    [SerializeField] private Path _path;

    private NavMeshAgent _meshAgent;
    private StateMachine _stateMachine;
    private Scout _scout;

    private float _healPoints = 100;
    private float _visibilityRange = 5f;
    private float _attackRange = 2f;
    private float _attackSpeed = 1f;
    private float _damage = 1f;

    public IEntity CurrentTarget { get; private set; }

    public override Vector3 Position => transform.position;

    public override event UnityAction<Warrior> Died;

    private void OnDisable()
    {
        _scout.ChangeTarget -= OnChangeTarget;
        _scout.OnDisable();
    }

    private void Update()
    {
        if( _stateMachine.CurrentState != null)
            _stateMachine.Update();
    }

    private void LateUpdate()
    {
        _scout.LateUpdate();
    }

    public override void InitUnit(Path path, Counter counter)
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _path = path;

        _scout = new Scout(counter, transform, _visibilityRange);
        _scout.ChangeTarget += OnChangeTarget;
        
        _stateMachine = new StateMachine(transform);
        _stateMachine.AddState(new AttackState(_stateMachine, _attackRange, _damage, _attackSpeed));
        _stateMachine.AddState(new WalkState(_stateMachine, _path, _meshAgent, _attackRange));
        _stateMachine.SetState<WalkState>();
    }

    public override void GetDamage(float damage)
    {
        _healPoints -= damage;

        if (_healPoints <= 0)
            Died?.Invoke(this);
    }

    private void OnChangeTarget(IEntity entity)
    {
        _stateMachine.ChangeTarget(entity);
    }
}
