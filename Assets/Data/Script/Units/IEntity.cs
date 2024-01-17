using UnityEngine;
using UnityEngine.Events;

public interface IEntity
{
    public Vector3 Position { get; }

    public void GetDamage(float damage);
}
