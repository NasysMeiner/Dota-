using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatUp/DoubleAttack")]
public class DoubleAttackData : StatUp
{
    public override void LevelUpStat(WarriorData warriorData)
    {
        warriorData.IsDoubleAttack = true;
    }
}
