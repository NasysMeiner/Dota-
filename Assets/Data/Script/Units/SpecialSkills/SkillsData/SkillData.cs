using UnityEngine;

public class SkillData : ScriptableObject, ISkill
{
    public bool IsPurchased = false;
    public int Price;
    public TypeSkill TypeSkill;
    public Skill PrefabSkill;
    public ContainerSkill ContainerSkill;

    public int PriceSkill => Price;

    bool ISkill.IsPurchased => IsPurchased;

    public virtual void LoadData(Skill skill) { }
}
