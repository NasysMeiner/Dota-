using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skills/LiveAfterDeath")]
public class LiveAfterDeathData : StatUp
{
    public float TimeImmortaly = 3f;

    public override void LevelUpStat(WarriorData warriorData)
    {
        warriorData.TimeImmortaly = TimeImmortaly;
    }
}
