using UnityEngine;

public class ArcherAttackState : AttackState
{
    private Archer _archer;

    public ArcherAttackState(StateMachine stateMachine, Effect atackEffect) : base(stateMachine, atackEffect) { }

    protected override void MakeDamage()
    {
        if (_archer == null)
            _archer = _stateMachine.Warrior as Archer;

        Bullet bullet = _archer.CreateBullet();
        bullet.transform.position = _archer.Position;
        Vector3 targetPosition = CalculeutVector(bullet);
        bullet.Initialization(_archer.CurrentTarget, targetPosition, _archer.Damage, _archer.AttckRange);
    }

    private Vector3 CalculeutVector(Bullet bullet)
    {
        Vector3 resultVector = _archer.CurrentTarget.Position;

        if (_archer.CurrentTarget as Unit != null)
        {
            Unit target = _archer.CurrentTarget as Unit;

            float time = (_archer.CurrentTarget.Position - _archer.Position).magnitude / bullet.Speed;

            float x = target.Position.x + time * target.MeshAgent.velocity.x;
            float y = target.Position.y + time * target.MeshAgent.velocity.x;

            resultVector = new Vector3(x, y, target.Position.z);
        }

        return resultVector;
    }
}
