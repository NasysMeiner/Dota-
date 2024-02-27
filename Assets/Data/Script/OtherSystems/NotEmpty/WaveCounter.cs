using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    private List<Barrack> _playerBarracks;
    private List<Barrack> _aIBarracks;

    private int _playerBarrackCount = 0;
    private int _aIBarrackCount = 0;

    private int _maxCountPlayer;
    private int _maxCountAI;

    private bool _isWait = false;

    private void OnDisable()
    {
        if (_isWait)
        {
            foreach (Barrack barrack in _playerBarracks)
                barrack.EndWave -= OnEndWave;

            foreach (Barrack barrack in _aIBarracks)
                barrack.EndWave -= OnEndWave;
        }
    }

    public void InitWaveCounter(List<Barrack> playerBarracks, List<Barrack> AIBarracs, bool isWait = false)
    {
        _isWait = isWait;

        _playerBarracks = playerBarracks;
        _aIBarracks = AIBarracs;

        _maxCountPlayer = _playerBarracks.Count;
        _maxCountAI = _aIBarracks.Count;

        if (_isWait)
        {
            foreach (Barrack barrack in _playerBarracks)
                barrack.EndWave += OnEndWave;

            foreach (Barrack barrack in _aIBarracks)
                barrack.EndWave += OnEndWave;
        }
    }

    private void OnEndWave(Barrack barrack)
    {
        if (barrack.Name == _playerBarracks[0].Name)
            _playerBarrackCount++;
        else
            _aIBarrackCount++;

        Debug.Log("чек");
        CheckFullEndWave();
    }

    private void CheckFullEndWave()
    {
        if (_playerBarrackCount == _maxCountPlayer && _aIBarrackCount == _maxCountAI && _isWait)
        {
            Debug.Log("Запускаю");

            foreach (Barrack barrack in _playerBarracks)
                barrack.ContinueSpawn();

            foreach (Barrack barrack in _aIBarracks)
                barrack.ContinueSpawn();

            _playerBarrackCount = 0;
            _aIBarrackCount = 0;
        }

    }
}
