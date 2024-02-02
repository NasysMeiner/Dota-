using UnityEngine.Events;

public interface IUnit
{
    public event UnityAction<Warrior> Died;
    public void InitUnit(Path path, Counter counter, WarriorData warriorData, int id, string name);
}
