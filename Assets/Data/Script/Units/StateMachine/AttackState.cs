using System;
using UnityEngine;

public class AttackState : State
{
    private float _attackRange;
    private float _damage;
    private float _attackSpeed;

    private float _time = 0;

    public AttackState(StateMachine stateMachine, float attackRange, float damage, float attackSpeed) : base(stateMachine)
    {
        _attackRange = attackRange;
        _damage = damage;
        _attackSpeed = attackSpeed;
    }

    public override void Enter()
    {
        if (_stateMachine.Target != null && Math.Abs((_stateMachine.Transform.position - _stateMachine.Target.Position).magnitude) > _attackRange / 2)
            _stateMachine.SetState<WalkState>();

        _isWork = true;
    }

    public override void Exit()
    {
        _isWork = false;
    }

    public override void Update()
    {
        if(_isWork)
        {
            _time += Time.deltaTime;

            if (_stateMachine.Target != null && Math.Abs((_stateMachine.Transform.position - _stateMachine.Target.Position).magnitude) <= _attackRange && _time >= _attackSpeed)
            {
                _time = 0;
                _stateMachine.Target.GetDamage(_damage);
            }
            else
            {
                _stateMachine.SetState<WalkState>();
            }
        }
        
    }
}
