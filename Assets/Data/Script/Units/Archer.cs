using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        AttackState archerAttackState = new ArcherAttackState(_stateMachine, _bullet);
        Debug.Log(archerAttackState);
        _stateMachine.AddState(archerAttackState);
        _stateMachine.AddState(new WalkState(_stateMachine, _path, _meshAgent, SearchStartPointId()));
        _stateMachine.AddState(new IdleState(_stateMachine));

        _stateMachine.SetState<WalkState>();
    }

    public Bullet CreateBullet()
    {
        return Instantiate(_bullet);
    }
}
