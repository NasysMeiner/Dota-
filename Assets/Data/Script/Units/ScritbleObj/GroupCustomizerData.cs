using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroupCustomizerData")]
public class GroupCustomizerData : ScriptableObject
{
    public List<Group> GroupUnits;
}

[System.Serializable]
public class Group
{
    public List<VariertyUnit> VariertyUnits;
}

