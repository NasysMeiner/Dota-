using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private Panel _victoryWindow;
    [SerializeField] private Panel _defeatWindow;

    private GameResult _result;

    public void InitGameResultView(GameResult result)
    {
        _result = result;

        _result.Defeat += OnDefeat;
        _result.Victory += OnVictory;
    }

    public void OnVictory()
    {
        _victoryWindow.OpenPanel();
    }

    public void OnDefeat()
    {
        _defeatWindow.OpenPanel();
    }
}
