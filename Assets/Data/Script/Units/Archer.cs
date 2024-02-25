public class Archer : Unit
{
    private Bullet _bullet;

    public override void LoadStats(WarriorData archerData)
    {
        base.LoadStats(archerData);

        ArcherData archer = archerData as ArcherData;
        _bullet = archer.Bullet;
    }

    protected override void CreateState()
    {
        _stateMachine.AddState(new ArcherAttackState(_stateMachine, _effectAttack));
        _stateMachine.AddState(new WalkState(_stateMachine, _targetPoint, _meshAgent));
        _stateMachine.AddState(new IdleState(_stateMachine));

        _stateMachine.SetState<WalkState>();
    }

    public Bullet CreateBullet()
    {
        return Instantiate(_bullet);
    }
}
