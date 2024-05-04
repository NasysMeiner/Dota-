using UnityEngine;
using UnityEngine.Events;

public class GameResult : MonoBehaviour
{
    private Castle _player;
    private Castle _ai;

    public event UnityAction Victory;
    public event UnityAction Defeat;

    public void InitGameResult(Castle player, Castle ai)
    {
        _player = player;
        _ai = ai;

        _player.CastleDestroyed += OnCastleDestroyed;
        _ai.CastleDestroyed += OnCastleDestroyed;
    }

    public void OnCastleDestroyed(string name)
    {
        _player.CastleDestroyed -= OnCastleDestroyed;
        _ai.CastleDestroyed -= OnCastleDestroyed;

        if (name == _player.Name)
            Defeat?.Invoke();
        else
            Victory?.Invoke();
    }
}
