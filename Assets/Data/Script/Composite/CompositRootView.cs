using System.Collections.Generic;
using UnityEngine;

public class CompositRootView : CompositeRoot
{
    [Header("View")]
    [SerializeField] private Castle _castlePlayer;
    [SerializeField] private List<Heart> _hearts;
    [SerializeField] private HealPointView _healPointView;
    [SerializeField] private GoldView _goldView;
    [SerializeField] private UpgrateStatsView _upgradeStatsView;

    [Header("Spawn")]
    [SerializeField] private List<ViewSprite> _images;
    [SerializeField] private ButtonUnitView _buttonUnitView;
    [SerializeField] private RadiusSpawner _radiusSpawner;

    [Header("Other System")]
    [SerializeField] private ChangerStats _changerStats;
    [SerializeField] private List<Content> _contents;
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
        _upgradeStatsView.InitUpgrateStatsView(_changerStats);
    }
}
