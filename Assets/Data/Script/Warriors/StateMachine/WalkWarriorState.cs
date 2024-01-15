using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkWarriorState : State
{
    private Path _path;
    private NavMeshAgent _meshAgent;
    private Transform _transform;

    private float _minDistance = 0.2f;
    private int _currentPointId = 0;
    private Vector3 _currentPoint;

    public WalkWarriorState(StateMachine stateMachine, Transform transform, Path path, NavMeshAgent agent) : base(stateMachine)
    {
        _path = path;
        _meshAgent = agent;
        _transform = transform;
    }

    public override void Enter()
    {
        _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;
    }

    public override void Exit()
    {
        _meshAgent.SetDestination(_transform.position);
    }

    public override void Update()
    {
        if (Math.Abs((_currentPoint - _transform.position).magnitude) < _minDistance && _currentPointId + 1 < _path.StandartPath.Count)
        {
            _currentPoint = _path.StandartPath[++_currentPointId].RandomPoint;
            _meshAgent.SetDestination(_currentPoint);
        }


    }
}
