using UnityEngine;

public class SkillData : ScriptableObject, ISkill
{
    public bool IsPurchased = false;
    public int Price;
    public TypeSkill TypeSkill;
    public Skill PrefabSkill;
    public ContainerSkill ContainerSkill;

    protected bool _isUnlock;

    public int PriceSkill => Price;

    bool ISkill.IsPurchased => IsPurchased;

    TypeSkill ISkill.TypeSkill => TypeSkill;

    public virtual void LoadData(Skill skill) { }

    public void InitData()
    {
        //_isUnlock = IsPurchased;
    }

    public void UnlockSkill()
    {
        IsPurchased = true;
    }
}
