using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataWarrior")]
public class WarriorData : ScriptableObject
{
    public TypeUnit Type;

    //public Color Color;

    public RuntimeAnimatorController Avatar;

    public Sprite Sprite;

    [Range(1, 100000)]
    public float HealPoint;

    [Range(0.1f, 100000)]
    public float AttackDamage;

    [Range(0.1f, 50)]
    public float AttackRange;

    [Range(0.1f, 1000)]
    public float VisibilityRange;

    [Range(0.1f, 1000)]
    public float AttackSpeed;

    [Range(1, 1000)]
    public float Speed;

    [Range(0.1f, 10)]
    public float ApproximationFactor;

    public Effect EffectDamage;

    public Effect EffectAttack;
}
