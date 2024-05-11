using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isLoad;
    [Space]
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private DataCountLevels _countLevels;
    [SerializeField] private DataCountLevels _currentData;
    [SerializeField] private LevelSelection _prefab;
    [SerializeField] private GameObject _content;

    private List<LevelSelection> _selectedLevels = new();

    private void Awake()
    {
        Repository.LoadState();
    }

    private void Start()
    {
        InitMenuLevel();
    }

    public void InitMenuLevel()
    {
        _currentData.LoadData(_countLevels);

        if (Repository.TryGetData(out DataScene dataScene))
            LoadDataDisk(dataScene);

        for (int i = 0; i < _currentData.Levels.Count; i++)
        {
            LevelSelection newSelection = Instantiate(_prefab, _content.transform);
            newSelection.SetLevel(_currentData.Levels[i], this);
            _selectedLevels.Add(newSelection);
        }

        SaveDataDisk();
    }

    public void LoadLevel(string name)
    {
        _audioManager.Stop();
        _sceneLoader.LoadLevel(name);
    }

    private void LoadDataDisk(DataScene dataScene)
    {
        _currentData.LoadData(dataScene);
    }

    private void SaveDataDisk()
    {
        DataScene dataScene = new();
        dataScene.InitDataScene(_currentData);

        Repository.SetData(dataScene);
    }
}

[System.Serializable]
public class DataScene
{
    public List<SceneCell> Scenes = new();

    public void InitDataScene(DataCountLevels dataCountLevels)
    {
        Scenes.Clear();

        foreach(LevelData levelData in dataCountLevels.Levels)
        {
            Scenes.Add(new SceneCell(levelData.Scene, levelData.status));
        }
    }
}

[System.Serializable]
public class SceneCell
{
    public string Name;
    public int Status;

    public SceneCell(string name, int status)
    {
        Name = name;
        Status = status;
    }
}
