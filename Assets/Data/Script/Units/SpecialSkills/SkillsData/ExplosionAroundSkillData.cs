using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skills/ExplosionAroundSkill")]
public class ExplosionAroundSkillData : SkillData
{
    public ExplosionAroundSkill PrefabSkill;

    [Header("Stat")]
    public float AttackScaling = 0.5f;

    public float Damage = 500;

    public float Radius = 2;

    public override void LoadData()
    {
        ContainerExplosionAroundSkill container = new()
        {
            Damage = Damage,
            Radius = Radius,
            AttackScaling = AttackScaling,
            TypeSkill = TypeSkill
        };
        Debug.Log(PrefabSkill.name + " " + TypeSkill);

        PrefabSkill.InitSkill(container);
    }
}

[System.Serializable]
public class ContainerExplosionAroundSkill : ContainerSkill
{
    public float AttackScaling = 4;

    public float Damage = 500;

    public float Radius = 2;
}
