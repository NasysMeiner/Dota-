using UnityEngine;

public class AttackState : State
{
    private float _time = 0;
    private Effect _effectAtack;

    public AttackState(StateMachine stateMachine, Effect attackEffect) : base(stateMachine)
    {
        StateName = "Attack";//��������
        _effectAtack = attackEffect;
    }

    public override void Enter()
    {
        if (_stateMachine.Warrior.CurrentTarget != null && Mathf.Abs((_stateMachine.Warrior.Position - _stateMachine.Warrior.CurrentTarget.Position).magnitude) > _stateMachine.Warrior.AttckRange * _stateMachine.Warrior.ApproximationFactor)
        {
            _stateMachine.SetState<WalkState>();
        }

        _isWork = true;
        _time = 0;
    }

    public override void Exit()
    {
        _isWork = false;
    }

    public override void Update()
    {
        if (_isWork)
        {
            if (_stateMachine.Warrior.CurrentTarget != null)
            {
                _time -= Time.deltaTime;
                float leanght = Mathf.Abs((_stateMachine.Warrior.Position - _stateMachine.Warrior.CurrentTarget.Position).magnitude);

                if (leanght <= _stateMachine.Warrior.AttckRange && _time <= 0)
                {
                    _time = _stateMachine.Warrior.AttackSpeed;
                    MakeDamage();
                    MakeEffectAtack();
                }
                else if (leanght > _stateMachine.Warrior.AttckRange)
                {
                    _stateMachine.SetState<WalkState>();
                }
            }
            else
            {
                _stateMachine.SetState<WalkState>();
            }
        }

    }

    protected virtual void MakeDamage()
    {
        _stateMachine.Warrior.CurrentTarget.GetDamage(_stateMachine.Warrior.Damage);
    }

    private void MakeEffectAtack()
    {
        _effectAtack.StartEffect();
    }
}
