using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataArcher")]
public class ArcherData : WarriorData
{
    [Header("Bullet")]
    public Bullet Bullet;

    public override void LoadStat(WarriorData warriorData)
    {
        base.LoadStat(warriorData);

        ArcherData archerData;

        if (archerData = warriorData as ArcherData)
        {
            Bullet = archerData.Bullet;
        }
        else
        {
            Debug.Log("Not load ;(");
        }
    }
}
