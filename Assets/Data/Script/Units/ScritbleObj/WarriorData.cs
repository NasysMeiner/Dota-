using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataWarrior")]
public class WarriorData : ScriptableObject
{
    public TypeUnit Type;

    public Color Color;

    [Range(1, 1000)]
    public float HealPoint;

    [Range(0.1f, 1000)]
    public float AttackDamage;

    [Range(0.1f, 5)]
    public float AttackRange;

    [Range(0.1f, 100)]
    public float VisibilityRange;

    [Range(0.1f, 100)]
    public float AttackSpeed;

    [Range(1, 100)]
    public float Speed;
}
