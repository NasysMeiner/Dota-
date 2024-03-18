public class IdleState : State
{
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
        StateName = "Idle";
    }

    public override void Enter()
    {
        StartAnimation();
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
