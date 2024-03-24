using UnityEngine;

public abstract class StatUp : ScriptableObject, ISkill
{
    public bool IsPurchased = false;
    public int Price;

    public int PriceSkill => Price;

    bool ISkill.IsPurchased => IsPurchased;

    public abstract void LevelUpStat(WarriorData warriorData);
}
