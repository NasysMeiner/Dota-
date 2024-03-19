using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skills/ConstantAreaDamageSkill")]
public class ConstantAreaDamageData : SkillData
{
    [Header("Stat")]
    public float AttackScaling = 0;

    public float Damage = 20;

    public float Time = 0.2f;

    public float Radius = 2;

    public override void LoadData(Skill skill)
    {
        ConstantAreaDamageSkill container = new()
        {
            Damage = Damage,
            Radius = Radius,
            AttackScaling = AttackScaling,
            Time = Time,
            TypeSkill = TypeSkill
        };

        Debug.Log(TypeSkill + " 1");


        ContainerSkill = container;

        skill.InitSkill(container);
    }
}

[System.Serializable]
public class ConstantAreaDamageSkill : ContainerSkill
{
    public float AttackScaling;

    public float Damage;

    public float Time;

    public float Radius;
}