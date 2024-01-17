using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    public abstract Vector3 Position { get; }

    public abstract event UnityAction<Warrior> Died;

    public abstract void GetDamage(float damage);
    public abstract void InitUnit(Path path, Counter counter);
}
