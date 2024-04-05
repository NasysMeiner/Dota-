using UnityEngine;

public class SkillData : ScriptableObject, ISkill
{
    public bool IsPurchased = false;
    public int Price;
    public Sprite IconSkill;
    public TypeSkill TypeSkill;
    public Skill PrefabSkill;
    public ContainerSkill ContainerSkill;

    protected bool _isUnlock;

    public int PriceSkill => Price;

    bool ISkill.IsPurchased => IsPurchased;

    TypeSkill ISkill.TypeSkill => TypeSkill;

    public Sprite Icon => IconSkill;

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
