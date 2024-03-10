using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatUp/ProjectileBlockChanceUp")]
public class ProjectileBlockChanceUpData : StatUp
{
    public float ProjectileBlockChance = 0;

    public override void LevelUpStat(WarriorData warriorData)
    {
        warriorData.ProjectileBlockChance = ProjectileBlockChance;

        if (ProjectileBlockChance > 100 || ProjectileBlockChance < 0)
            ProjectileBlockChance = 50;
    }
}
