using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : AttackState
{
    private Bullet _bullet;

    public ArcherAttackState(StateMachine stateMachine, Bullet bullet) : base(stateMachine)
    {
        _bullet = bullet;
    }

    //protected override void MakeDamage()
    //{
    //    //var bullet = Inst
    //}
}
