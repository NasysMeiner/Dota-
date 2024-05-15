using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private PanelLoad _victoryWindow;
    [SerializeField] private Panel _defeatWindow;
    [SerializeField] private Tutorial _tutorial;

    private GameResult _result;
    private string _currentSceneName;
    private string _nextSceneName;

    public void InitGameResultView(GameResult result)
    {
        _result = result;

        _result.Defeat += OnDefeat;
        _result.Victory += OnVictory;

        _currentSceneName = SceneManager.GetActiveScene().name;

        DataScene scene = Repository.GetData<DataScene>();

        if(scene.TryGetNextScene(_currentSceneName, out string nextScene))
        {
            _victoryWindow.OnLevelButton();
            _nextSceneName = nextScene;
        }
    }

    public void OpenTutorial()
    {
        _tutorial.OpenTutorial();
    }

    public void OnVictory()
    {
        Time.timeScale = 0;
        _victoryWindow.OpenPanel();

        DataScene scene = Repository.GetData<DataScene>();
        scene.UnlockNextLevel(_currentSceneName);
        Repository.SetData(scene);
    }

    public void OnDefeat()
    {
        Time.timeScale = 0;
        _defeatWindow.OpenPanel();
    }

    public void RestartLevel()
    {
        _sceneLoader.RestartLevel();
    }

    public void LoadLevel(string name)
    {
        _sceneLoader.LoadLevel(_nextSceneName);
    }

    public void LoadMainMenu()
    {
        _sceneLoader.LoadMainMenu();
    }
}
