using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartLevel()
    {
        Repository.SaveState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Repository.SaveState();
        Application.Quit();
    }

    public void LoadLevel(string name)
    {
        Repository.SaveState();
        SceneManager.LoadScene(name);
    }

    public void LoadMainMenu()
    {
        LoadLevel("MainMenu");
    }
}
