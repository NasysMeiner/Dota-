using UnityEngine.Events;

public interface IUnit
{
    public event UnityAction<Unit> Died;
    public void InitUnit(Path path, Counter counter, int id, string name);
}
