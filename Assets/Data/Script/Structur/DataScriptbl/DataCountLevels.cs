using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Levels")]
public class DataCountLevels : ScriptableObject
{
    public LevelData MainMenu;
    public List<LevelData> Levels = new();

    public void LoadData(DataScene dataScene)
    {
        for(int i = 0; i < Levels.Count; i++)
        {
            Levels[i].LoadData(dataScene.Scenes[i]);
        }
    }

    public void LoadData(DataCountLevels dataScene)
    {
        Levels.Clear();
        MainMenu = dataScene.MainMenu;

        for(int i = 0; i < dataScene.Levels.Count; i++)
            Levels.Add(new LevelData(dataScene.Levels[i].Scene, dataScene.Levels[i].status, dataScene.Levels[i].Icon));
    }
}

[System.Serializable]
public class LevelData
{
    public string Scene;
    public int status;
    public Sprite Icon;

    public LevelData(string scene, int status, Sprite icon)
    {
        Scene = scene;
        this.status = status;
        Icon = icon;
    }

    public void LoadData(SceneCell sceneCell)
    {
        Scene = sceneCell.Name;
        status = sceneCell.Status;
    }
}
