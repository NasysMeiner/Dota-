using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatUp/DoubleAttack")]
public class DoubleAttackData : StatUp
{
    public override void LevelUpStat(WarriorData warriorData)
    {
        Debug.Log("Yes!");
        warriorData.IsDoubleAttack = true;
    }
}
