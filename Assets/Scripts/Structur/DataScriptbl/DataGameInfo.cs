using UnityEngine;

[CreateAssetMenu(menuName = "Data/StartData")]
public class DataGameInfo : ScriptableObject
{
    public string Name;

    [Range(1000, 100000)]
    public int StartMoney;
}
