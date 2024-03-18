using UnityEngine;

public class SkillData : ScriptableObject
{
    public TypeSkill TypeSkill;
    public Skill PrefabSkill;
    public ContainerSkill ContainerSkill;

    public virtual void LoadData(Skill skill) { }
}
