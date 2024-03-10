using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatUp/AttackRangeUp")]
public class AttackRangeUpData : StatUp
{
    public float AttackRangeUp;

    public override void LevelUpStat(WarriorData warriorData)
    {
        warriorData.AttackRange += AttackRangeUp;

        if (warriorData.VisibilityRange < warriorData.AttackRange)
            warriorData.VisibilityRange = warriorData.AttackRange;
    }
}
