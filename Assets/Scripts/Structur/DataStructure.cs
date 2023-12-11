using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataStruct")]
public class DataStructure : ScriptableObject
{
    [Range(1, 100)]
    public int Income;
}
