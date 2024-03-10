using UnityEngine;

public interface IEntity
{
    public Vector3 Position { get; }
    public GameObject GameObject { get; }
    public bool IsAlive { get; }

    public void GetDamage(float damage, AttackType attackType);
    public void ChangePosition(Vector3 position);
}
