using System;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private NavMeshAgent _meshAgent;

    private Vector3 _targetPoint;
    private bool _isWalkToTarget = false;
    private bool _isEnd = false;
    private float _startSpeed;

    public WalkState(StateMachine stateMachine, Vector3 targetPoint, NavMeshAgent agent) : base(stateMachine)
    {
        _targetPoint = targetPoint;
        //Debug.Log(_targetPoint);
        _meshAgent = agent;
        _startSpeed = _meshAgent.speed;
        StateName = "Walk";//временно
    }

    public override void Enter()
    {
        if (_stateMachine.Warrior.CurrentTarget == null && _isEnd != true)
        {
            if (_meshAgent.SetDestination(_targetPoint))
                _meshAgent.SetDestination(_targetPoint);
        }
        else if (_stateMachine.Warrior.CurrentTarget != null)
        {
            if (_meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position))
            {
                _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);
                _isWalkToTarget = true;
            }
        }
        else
        {
            _stateMachine.SetState<IdleState>();
        }

        _meshAgent.speed = _startSpeed;
        _isWork = true;
    }

    public override void Exit()
    {
        _isWork = false;

        _isWalkToTarget = false;
        _meshAgent.SetDestination(_stateMachine.Warrior.Position);
        _meshAgent.speed = 0;
    }

    public override void Update()
    {
        if (_isWork)
        {
            if (_stateMachine.Warrior.CurrentTarget == null && _meshAgent.hasPath == false && _isEnd == false)
                _meshAgent.SetDestination(_targetPoint);
            else if (_stateMachine.Warrior.CurrentTarget != null && _meshAgent.hasPath == false)
                _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);

            if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == false)
                _stateMachine.SetState<AttackState>();
            else if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == true && Math.Abs((_stateMachine.Warrior.Position - _stateMachine.Warrior.CurrentTarget.Position).magnitude) <= _stateMachine.Warrior.AttckRange * _stateMachine.Warrior.ApproximationFactor)
                _stateMachine.SetState<AttackState>();

            if (_stateMachine.Warrior.CurrentTarget == null && _isEnd)
                _stateMachine.SetState<IdleState>();

            if (Mathf.Abs((_targetPoint - _stateMachine.Warrior.Position).magnitude) < 0.1f)
                _isEnd = true;
        }
    }
}