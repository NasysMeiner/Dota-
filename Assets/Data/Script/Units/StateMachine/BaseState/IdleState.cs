using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
        StateName = "Idle";
    }

    public override void Enter()
    {
        _isWork = true;
    }

    public override void Exit()
    {
        _isWork = false;
    }

    public override void Update()
    {
        if (_stateMachine.Warrior.CurrentTarget != null)
            _stateMachine.SetState<AttackState>();
    }
}
