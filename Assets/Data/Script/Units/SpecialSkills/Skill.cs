using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected bool _isActive;

    public TypeSkill TypeSkill {  get; protected set; }

    public abstract void InitSkill(ContainerSkill container);
    public abstract void SetUnit(Unit unit);
    public abstract void UseSkill();

    public virtual void StopSkill()
    {
        _isActive = false;
    }
}

[System.Serializable]
public class ContainerSkill
{
    public TypeSkill TypeSkill;
}

public enum TypeSkill
{
    Init,
    Deatch
}