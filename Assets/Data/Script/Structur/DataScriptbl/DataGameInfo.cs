using UnityEngine;

[CreateAssetMenu(menuName = "Data/StartData")]
public class DataGameInfo : ScriptableObject
{
    public string Name;

    public Color ColorUnit;

    [Range(1000, 100000)]
    public int StartMoney;

    public DataStructure DataStructure;
}
