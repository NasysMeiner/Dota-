using System;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private Path _path;
    private NavMeshAgent _meshAgent;

    private int _currentPointId;
    private Vector3 _currentPoint;
    private bool _isWalkToTarget = false;
    private bool _isEnd = false;
    private int _nextPointConst;
    private float _startSpeed;

    public WalkState(StateMachine stateMachine, Path path, NavMeshAgent agent, int startNumberPoint) : base(stateMachine)
    {
        _path = path;
        _meshAgent = agent;
        _currentPointId = startNumberPoint;
        _nextPointConst = startNumberPoint > 0 ? -1 : 1;
        _startSpeed = _meshAgent.speed;
        StateName = "Walk";//временно
    }

    public override void Enter()
    {
        if (_stateMachine.Warrior.CurrentTarget == null && _currentPointId < _path.StandartPath.Count && _currentPointId >= 0)
        {
            _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;
            _stateMachine.Warrior._pointId = _currentPointId;

            if (_meshAgent.SetDestination(_currentPoint))
                _meshAgent.SetDestination(_currentPoint);
        }
        else if (_stateMachine.Warrior.CurrentTarget != null)
        {
            if (_meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position))
                _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);

            _isWalkToTarget = true;
        }
        else
        {
            _stateMachine.SetState<IdleState>();
            //Debug.Log("END Enter " + _stateMachine.Warrior._name + " :" + _stateMachine.Warrior._id + " " + _currentPointId + " " + _path.StandartPath.Count);
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
            if (_stateMachine.Warrior.CurrentTarget != null && _meshAgent.hasPath == false)
                _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);

            if (_stateMachine.Warrior.CurrentTarget == null && _isEnd)
                _stateMachine.SetState<IdleState>();
            else if (_stateMachine.Warrior.CurrentTarget == null && _meshAgent.hasPath == false && _isEnd == false)
                _meshAgent.SetDestination(_currentPoint);
            else if (_stateMachine.Warrior.CurrentTarget != null && _meshAgent.hasPath == false)
                _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);

            if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == false)
                _stateMachine.SetState<AttackState>();
            else if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == true && Math.Abs((_stateMachine.Warrior.Position - _stateMachine.Warrior.CurrentTarget.Position).magnitude) <= _stateMachine.Warrior.AttckRange / 2)
                _stateMachine.SetState<AttackState>();

            if (_path.StandartPath[_currentPointId].CheckPointInLine(_stateMachine.Warrior.Position))
            {
                if (_currentPointId + _nextPointConst < _path.StandartPath.Count && _currentPointId + _nextPointConst >= 0)
                {
                    _currentPointId += _nextPointConst;
                    _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;

                    _stateMachine.Warrior._pointId = _currentPointId;

                    if (_stateMachine.Warrior.CurrentTarget == null)
                        if (_meshAgent.SetDestination(_currentPoint))
                            _meshAgent.SetDestination(_currentPoint);
                }
                else if (_currentPointId == _path.StandartPath.Count - 1 || _currentPointId == 0)
                {
                    _isEnd = true;
                }
            }
        }
    }
}