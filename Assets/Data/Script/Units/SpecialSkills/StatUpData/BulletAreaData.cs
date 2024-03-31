using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatUp/BulletArea")]
public class BulletAreaData : StatUp
{
    public Bullet AreaBullet;

    public override void LevelUpStat(WarriorData warriorData)
    {
        ArcherData archerData = warriorData as ArcherData;
        archerData.Bullet = AreaBullet;
        Debug.Log(archerData.Bullet);
    }
}
