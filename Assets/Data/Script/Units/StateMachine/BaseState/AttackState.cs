using UnityEngine;

public class AttackState : State
{
    private float _time = 0;
    private Effect _effectAtack;
    private bool _isDoubleAttack = false;

    public AttackState(StateMachine stateMachine, Effect attackEffect) : base(stateMachine)
    {
        StateName = "Attack";//временно
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
                    MakeDamage();
                    MakeEffectAtack();

                    if (_stateMachine.Warrior.IsDoubleAttack && _isDoubleAttack == false)
                    {
                        _isDoubleAttack = true;
                        _time = 0.3f;
                    }
                    else
                    {
                        _isDoubleAttack = false;
                        _time = _stateMachine.Warrior.AttackSpeed;
                    }
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
        _stateMachine.Warrior.CurrentTarget.GetDamage(_stateMachine.Warrior.Damage, AttackType.Melee);
    }

    private void MakeEffectAtack()
    {
        if (_effectAtack != null)
            _effectAtack.StartEffect();
    }
}
