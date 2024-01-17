using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private Path _path;
    private NavMeshAgent _meshAgent;

    private float _minDistance = 0.2f;
    private int _currentPointId = 0;
    private Vector3 _currentPoint;
    private Vector3 _currentPoint2;
    private bool _isWalkToTarget = false;

    private float _attackRange;

    public WalkState(StateMachine stateMachine, Path path, NavMeshAgent agent, float attackRange) : base(stateMachine)
    {
        _path = path;
        _meshAgent = agent;
        _attackRange = attackRange;
    }

    public override void Enter()
    {
        _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;

        if(_stateMachine.Target == null)
        {
            _meshAgent.SetDestination(_currentPoint);
        }
        else
        {
            _meshAgent.SetDestination(_stateMachine.Target.Position);
            _isWalkToTarget = true;
        }

        _isWork = true;
    }

    public override void Exit()
    {
        _meshAgent.SetDestination(_stateMachine.Transform.position);
        _isWalkToTarget = false;

        _isWork = false;
    }

    public override void Update()
    {
        if (_isWork)
        {
            if (Math.Abs((_currentPoint - _stateMachine.Transform.position).magnitude) < _minDistance && _currentPointId + 1 < _path.StandartPath.Count)
            {
                _currentPoint = _path.StandartPath[++_currentPointId].RandomPoint;

                if (_stateMachine.Target == null)
                {
                    _meshAgent.SetDestination(_currentPoint);

                    return;
                }
            }

            //float leangth = Math.Abs((_stateMachine.Transform.position - _stateMachine.Target.Position).magnitude);

            if (_stateMachine.Target != null && _isWalkToTarget == false)
                _stateMachine.SetState<AttackState>();
            else if (_stateMachine.Target != null && _isWalkToTarget == true && Math.Abs((_stateMachine.Transform.position - _stateMachine.Target.Position).magnitude) <= _attackRange / 2)
                _stateMachine.SetState<AttackState>();
        }
    }
}
