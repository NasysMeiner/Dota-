using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public abstract event UnityAction<Warrior> Died;

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public abstract void GetDamage(float damage);
    public abstract void InitUnit(Path path, Counter counter, int id, string name);
}
