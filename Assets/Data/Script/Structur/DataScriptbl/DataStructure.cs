using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataStruct")]
public class DataStructure : ScriptableObject
{
    [Range(1, 100)]
    public int Income;

    [Range(1, 100)]
    public float MaxHealpPoint;

    public Effect EffectDamage; 

    public string Name { get; set; }
}
