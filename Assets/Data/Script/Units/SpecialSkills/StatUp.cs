using UnityEngine;

public abstract class StatUp : ScriptableObject, ISkill
{
    public bool IsPurchased;
    public Sprite IconSkill;
    public int Price;
    public string TextSkill;

    protected bool _isUnlock;

    public int PriceSkill => Price;

    bool ISkill.IsPurchased => IsPurchased;

    public TypeSkill TypeSkill => TypeSkill.StatsUp;

    public Sprite Icon => IconSkill;

    string ISkill.TextSkill => TextSkill;

    public abstract void LevelUpStat(WarriorData warriorData);

    public void InitData()
    {
        //_isUnlock = IsPurchased;
    }

    public void UnlockSkill()
    {
        IsPurchased = true;
    }
}
