using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositRootCastle : CompositeRoot
{
    [Header("CastlePlayer")]
    [SerializeField] private DataGameInfo _gameInfoPlayer;
    [SerializeField] private PointCreator _pointCreatorPlayer;
    [SerializeField] private Castle _castlePlayer;

    [Header("StructPlayer")]
    [SerializeField] private BarracksData _barracksDataPlayer;
    [SerializeField] private List<Barrack> _barracksPlayer;

    [Header("CastleAi")]
    [SerializeField] private DataGameInfo _gameInfoAi;
    [SerializeField] private PointCreator _pointCreatorAi;
    [SerializeField] private Castle _castleAi;

    [Header("StructAi")]
    [SerializeField] private BarracksData _barracksDataAi;
    [SerializeField] private List<Barrack> _barracksAi;

    [Header("Other")]
    [SerializeField] private Trash _trash;
    [SerializeField] private BarricadeBuilder _barricadeBuilder;
    [SerializeField] private int _numberBarricade;
    [SerializeField] private Barricade _barricadePrefab;
    [SerializeField] private WaveCounter _waveCounter;

    [Header("Towers")]
    [SerializeField] private List<Tower> _towers;

    public override void Compose()
    {
        InitializePlayer();
        InitializeAi();
        InitializeData();
    }

    private void InitializePlayer()
    {
        _barracksDataPlayer.WriteData(_pointCreatorPlayer);
        _castlePlayer.InitializeCastle(_gameInfoPlayer, _barracksPlayer, _towers, _barracksDataPlayer, _trash, _pointCreatorPlayer);
        _barricadeBuilder.InitBuilder(_barricadePrefab, _numberBarricade);
    }

    private void InitializeAi()
    {
        _barracksDataAi.WriteData(_pointCreatorAi);
        _castleAi.InitializeCastle(_gameInfoAi, _barracksAi, null, _barracksDataAi, _trash, _pointCreatorAi);
    }

    private void InitializeData()
    {
        _castlePlayer.InitializeEvent();
        _castleAi.SetEnemyCounter(_castlePlayer.Counter);
        _castlePlayer.SetEnemyCounter(_castleAi.Counter);
        _waveCounter.InitWaveCounter(_barracksPlayer, _barracksAi, _barracksDataPlayer.IsWait);
    }
}
