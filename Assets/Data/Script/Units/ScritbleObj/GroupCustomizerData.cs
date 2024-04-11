using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GroupCustomizerData")]
public class GroupCustomizerData : ScriptableObject
{
    public List<Group> GroupUnits;

    //public void InitGroupPrice(DataUnitStats dataUnitStats)
    //{

    //}
}

[System.Serializable]
public class Group
{
    public List<VariertyUnit> VariertyUnits;

    private int _price;

    public int PriceGroup => _price;
}

