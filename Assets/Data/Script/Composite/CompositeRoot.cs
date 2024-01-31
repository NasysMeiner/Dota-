using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [Header("CastlePlayer")]
    [SerializeField] private DataStructure _dataCastlePlayer;
    [SerializeField] private DataGameInfo _gameInfoPlayer;
    [SerializeField] private Castle _castlePlayer;

    [Header("StructPlayer")]
    [SerializeField] private DataStructure _dataStructurePlayer;
    [SerializeField] private List<Barrack> _barracksPlayer;

    [Header("CastleAi")]
    [SerializeField] private DataStructure _dataCastleAi;
    [SerializeField] private DataGameInfo _gameInfoAi;
    [SerializeField] private Castle _castleAi;

    [Header("StructAi")]
    [SerializeField] private DataStructure _dataStructureAi;
    [SerializeField] private List<Barrack> _barracksAi;

    [Header("Path")]
    [SerializeField] private List<Path> _path;

    [Header("View")]
    [SerializeField] private List<Heart> _hearts;
    [SerializeField] private HealPointView _healPointView;

    [Header("Units")]
    [SerializeField] private Warrior _prefabWarrior;

    [Header("Towers")]
    [SerializeField] private List<Tower> _towers;

    [Header("Other")]
    [SerializeField] private Trash _trash;
    [SerializeField] private BarricadeBuilder _barricadeBuilder;
    [SerializeField] private int _numberBarricade;
    [SerializeField] private Barricade _barricadePrefab;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach(var path in _path)
        {
            path.SearchPoint();
        }

        InitializePlayer();
        InitializeAi();
        InitializeView();
        InitializeData();
        StartGame();
    }

    private void InitializePlayer()
    {
        _castlePlayer.InitializeCastle(_dataCastlePlayer, _gameInfoPlayer, _barracksPlayer, _towers, _dataStructurePlayer, _path, _prefabWarrior, _trash);
        _barricadeBuilder.InitBuilder(_barricadePrefab, _numberBarricade);
    }

    private void InitializeAi()
    {
        _castleAi.InitializeCastle(_dataCastleAi, _gameInfoAi, _barracksAi, null, _dataStructureAi, _path, _prefabWarrior, _trash);
    }

    private void InitializeView()
    {
        _healPointView.Initialize(_castlePlayer, _hearts);
    }

    private void InitializeData()
    {
        _castlePlayer.InitializeEvent();
        _castleAi.SetEnemyCounter(_castlePlayer.Counter);
        _castlePlayer.SetEnemyCounter(_castleAi.Counter);
    }

    private void StartGame()
    {
        foreach (var barrack in _barracksPlayer)
        {
            barrack.isEnemy = false;
            barrack.SpawnUnits();
        }

        foreach (var barrack in _barracksAi)
        {
            barrack.isEnemy = true;
            barrack.SpawnUnits();
        }

        //_barracksPlayer[0].SpawnUnits();
        //_barracksAi[0].SpawnUnits();
    }
}
