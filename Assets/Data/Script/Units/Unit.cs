using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour, IUnit, IEntity
{
    public bool isEnemy;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    public abstract event UnityAction<Warrior> Died;

    public abstract void ChangePosition(Vector3 position);
    public abstract void GetDamage(float damage);
    public abstract void InitUnit(Path path, Counter counter, WarriorData warriorData, int id, string name);
}
