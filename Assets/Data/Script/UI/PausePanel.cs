using UnityEngine;

public class PausePanel : Panel
{
    [SerializeField] private SceneLoader _loader;

    public override void OpenPanel()
    {
        base.OpenPanel();
        Time.timeScale = 0;
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        _loader.RestartLevel();
    }

    public void ReturnMainMenu()
    {
        _loader.LoadMainMenu();
    }
}
