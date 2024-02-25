using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataUnitPrefab")]
public class DataUnitPrefab : ScriptableObject
{
    [Header("UnitPrefab")]
    public List<PrefabUnit> Prefabs = new List<PrefabUnit>();
}