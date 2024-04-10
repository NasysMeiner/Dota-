using System.Collections.Generic;
using UnityEngine;

public class CompositRootView : CompositeRoot
{
    [Header("View")]
    [SerializeField] private Castle _castlePlayer;
    [SerializeField] private Castle _castleAi;
    [SerializeField] private List<Heart> _hearts;
    [SerializeField] private HealPointView _healPointView;
    [SerializeField] private GoldView _goldView;
    [SerializeField] private UpgrateStatsView _upgradeStatsView;
    [Space]
    [SerializeField] private GameResult _gameResult;
    [SerializeField] private GameResultView _gameResultView;

    [Header("Spawn")]
    [SerializeField] private List<ViewSprite> _images;
    [SerializeField] private ButtonUnitView _buttonUnitView;
    [SerializeField] private RadiusSpawner _radiusSpawner;

    [Header("Other System")]
    [SerializeField] private ChangerStats _changerStats;
    [SerializeField] private UnitStatsBlock _unitStatsBlockPrefab;

    public override void Compose()
    {
        InitializeView();
    }

    private void InitializeView()
    {
        _healPointView.Initialize(_castlePlayer, _hearts);
        _buttonUnitView.Init(_radiusSpawner, _images);
        _goldView.InitGoldView(_castlePlayer.CashAccount);
        _upgradeStatsView.InitUpgrateStatsView(_changerStats, _radiusSpawner);
        _gameResult.InitGameResult(_castlePlayer, _castleAi);
        _gameResultView.InitGameResultView(_gameResult);
    }
}
