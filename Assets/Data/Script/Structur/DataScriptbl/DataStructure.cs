using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataStruct")]
public class DataStructure : ScriptableObject
{
    [Range(1, 10000)]
    public int Income;

    [Range(1, 10000)]
    public float MaxHealpPoint;

    public Effect EffectDamage;

    public Effect EffectDestruct;

    public Effect EffectStart;

    public float TimeStartEffect;

    public string Name { get; set; }
}
