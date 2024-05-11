using UnityEngine;
using UnityEngine.UI;

public class PanelLoad : Panel
{
    [SerializeField] private GameResultView _view;
    [SerializeField] private Button _nextLevelButton;

    public void RestartLevel()
    {
        _view.RestartLevel();
    }

    public void LoadLevel()
    {
        _view.LoadLevel("-");
    }

    public void LoadMainMenu()
    {
        _view.LoadMainMenu();
    }

    public void OnLevelButton()
    {
        _nextLevelButton.gameObject.SetActive(true);
    }
}
