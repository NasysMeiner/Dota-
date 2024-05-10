using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Levels")]
public class DataCountLevels : ScriptableObject
{
    public LevelData MainMenu;
    public List<LevelData> Levels;
}

[System.Serializable]
public class LevelData
{
    public string Scene;
    public Sprite Icon;
}
