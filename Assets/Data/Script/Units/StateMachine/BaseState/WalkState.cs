using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private Path _path;
    private NavMeshAgent _meshAgent;

    private float _minDistance = 0.2f;
    private int _currentPointId;
    private Vector3 _currentPoint;
    private bool _isWalkToTarget = false;
    private bool _isEnd = false;
    private int _nextPointConst;

    public WalkState(StateMachine stateMachine, Path path, NavMeshAgent agent, int startNumberPoint) : base(stateMachine)
    {
        _path = path;
        _meshAgent = agent;
        _currentPointId = startNumberPoint;
        _nextPointConst = startNumberPoint > 0 ? -1 : 1;
    }

    public override void Enter()
    {
        if (_stateMachine.Warrior.CurrentTarget == null && _currentPointId < _path.StandartPath.Count)
        {
            _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;
            _currentPointId += _nextPointConst;
            _meshAgent.SetDestination(_currentPoint);
        }
        else if(_stateMachine.Warrior.CurrentTarget != null)
        {
            _meshAgent.SetDestination(_stateMachine.Warrior.CurrentTarget.Position);
            _isWalkToTarget = true;
        }
        else
        {
            _stateMachine.SetState<IdleState>();

            return;
        }

        _isWork = true;
    }

    public override void Exit()
    {
        _isWork = false;

        _isWalkToTarget = false;
        _meshAgent.SetDestination(_stateMachine.Warrior.Position);
    }

    public override void Update()
    {
        if (_isWork)
        {
            float leangth = Math.Abs((_currentPoint - _stateMachine.Transform.position).magnitude);

            if ((leangth <= _minDistance && _stateMachine.Warrior.CurrentTarget == null) || (leangth <= _path.StandartPath[Mathf.Clamp(_currentPointId, 0, _path.StandartPath.Count - 1)].MaxLeangth && _stateMachine.Warrior.CurrentTarget != null))
            {
                if (_currentPointId < _path.StandartPath.Count && _currentPointId >= 0)
                {
                    _currentPoint = _path.StandartPath[_currentPointId].RandomPoint;
                    _currentPointId += _nextPointConst;

                    if (_stateMachine.Warrior.CurrentTarget == null)
                        _meshAgent.SetDestination(_currentPoint);
                    else if (_currentPointId >= _path.StandartPath.Count || _currentPointId < 0)
                        _isEnd = true;
                }
            }

            if (_stateMachine.Warrior.CurrentTarget == null && _isEnd)
                _stateMachine.SetState<IdleState>();

            if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == false)
                _stateMachine.SetState<AttackState>();
            else if (_stateMachine.Warrior.CurrentTarget != null && _isWalkToTarget == true && Math.Abs((_stateMachine.Transform.position - _stateMachine.Warrior.CurrentTarget.Position).magnitude) <= _stateMachine.Warrior.AttckRange / 2)
                _stateMachine.SetState<AttackState>();
        }
    }
}
