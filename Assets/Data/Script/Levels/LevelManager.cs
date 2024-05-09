using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private DataCountLevels _countLevels;
    [SerializeField] private LevelSelection _prefab;
    [SerializeField] private GameObject _content;

    private List<LevelSelection> _selectedLevels = new();

    private void Start()
    {
        InitMenuLevel();
    }

    public void InitMenuLevel()
    {
        for(int i = 0; i < _countLevels.Levels.Count; i++)
        {
            LevelSelection newSelection = Instantiate(_prefab, _content.transform);
            newSelection.SetLevel(_countLevels.Levels[i].Scene, _countLevels.Levels[i].Icon, this);
            _selectedLevels.Add(newSelection);
        }

        LoadJsonData();
    }

    public void LoadLevel(string name)
    {
        _audioManager.Stop();
        _sceneLoader.LoadLevel(name);
    }

    private void LoadJsonData()
    {
        //???
    }
}
