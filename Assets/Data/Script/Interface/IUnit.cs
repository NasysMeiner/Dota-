using UnityEngine;
using UnityEngine.Events;

public interface IUnit
{
    public event UnityAction<Unit> Died;
    public void InitUnit(Vector3 targetPoint, Counter counter, int id, string name);
}
