using UnityEngine.Events;

public interface IUnit
{
    public event UnityAction<Warrior> Died;
    public void InitUnit(Path path, Counter counter, int id, string name);
}
