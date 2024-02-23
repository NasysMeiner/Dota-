using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : AttackState
{
    private Archer _archer;

    public ArcherAttackState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void MakeDamage()
    {
        if(_archer == null)
            _archer = _stateMachine.Warrior as Archer;

        Bullet bullet = _archer.CreateBullet();
        bullet.transform.position = _archer.Position;
        bullet.Initialization(_archer.CurrentTarget, _archer.Damage, _archer.AttckRange);
    }
}
