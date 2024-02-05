using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : AttackState
{
    private Bullet _bullet;
    private Archer _archer;

    public ArcherAttackState(StateMachine stateMachine, Bullet bullet) : base(stateMachine)
    {
        _bullet = bullet;
    }

    protected override void MakeDamage()
    {
        if(_archer == null)
            _archer = _stateMachine.Warrior as Archer;

        Bullet bullet = _archer.CreateBullet();
        bullet.transform.position = _archer.Position;
        bullet.Initialization(_archer.CurrentTarget, _archer.Damage);
    }
}
