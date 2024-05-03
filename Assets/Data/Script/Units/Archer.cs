using UnityEngine;

public class Archer : Unit
{
    private Bullet _bullet;

    public override void LoadStats(WarriorData archerData)
    {
        ArcherData archer = archerData as ArcherData;
        _bullet = archer.Bullet;

        base.LoadStats(archerData);
    }

    protected override void CreateState()
    {
        State state = new ArcherAttackState(_stateMachine, _effectAttack, _isDoubleAttack);
        state.StateActive += _animateChanger.OnPlayHit;
        _stateMachine.AddState(state);

        state = new WalkState(_stateMachine, _targetPoint, _meshAgent);
        _stateMachine.AddState(state);
        state.StateActive += _animateChanger.OnPlayWalk;

        state = new IdleState(_stateMachine);
        _stateMachine.AddState(state);
        state.StateActive += _animateChanger.OnPlayIdle;

        state = new DeathState(_stateMachine);
        _stateMachine.AddState(state);
        state.StateActive += _animateChanger.OnPlayDeath;

        _stateMachine.SetState<WalkState>();
    }

    public Bullet CreateBullet()
    {
        return Instantiate(_bullet);
    }

    protected override void Die()
    {
        base.Die();

        if(_isChangeShadowDie)
            _shadow.transform.localPosition = new Vector3(0.23f, -2.47f, 0f);
    }
}
