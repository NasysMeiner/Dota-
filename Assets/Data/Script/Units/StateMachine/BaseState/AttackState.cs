using System;
using UnityEngine;

public class AttackState : State
{
    private float _time = 0;

    public AttackState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if (_stateMachine.Warrior.CurrentTarget != null && Math.Abs((_stateMachine.Transform.position - _stateMachine.Warrior.CurrentTarget.Position).magnitude) > _stateMachine.Warrior.AttckRange / 2)
            _stateMachine.SetState<WalkState>();

        _isWork = true;
    }

    public override void Exit()
    {
        _isWork = false;
    }

    public override void Update()
    {
        if(_isWork && _stateMachine != null && _stateMachine.Transform != null)
        {
            if (_stateMachine.Warrior.CurrentTarget != null)
            {
                _time -= Time.deltaTime;
                float leanght = Math.Abs((_stateMachine.Transform.position - _stateMachine.Warrior.CurrentTarget.Position).magnitude);

                if (leanght <= _stateMachine.Warrior.AttckRange && _time <= 0)
                {
                    _time = _stateMachine.Warrior.AttackSpeed;
                    //Debug.Log("Attack " + _stateMachine.Warrior.CurrentTarget + " Damage: " + _stateMachine.Warrior.Damage);
                    _stateMachine.Warrior.CurrentTarget.GetDamage(_stateMachine.Warrior.Damage);
                }
                else if(leanght > _stateMachine.Warrior.AttckRange)
                {
                    _stateMachine.SetState<WalkState>();
                }
            }
            else if(_stateMachine.Warrior.CurrentTarget == null)
            {
                _stateMachine.SetState<WalkState>();
            }
        }
        
    }
}
