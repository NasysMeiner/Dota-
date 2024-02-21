using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataUnitStats")]
public class DataUnitStats : ScriptableObject
{
    public List<StatsPrefab> StatsPrefab = new List<StatsPrefab>();
}
