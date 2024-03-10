using UnityEngine;

public abstract class StatUp : ScriptableObject
{
    public bool IsPurchased = false;

    public abstract void LevelUpStat(WarriorData warriorData);
}
