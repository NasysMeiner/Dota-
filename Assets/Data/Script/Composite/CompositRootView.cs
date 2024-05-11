using System.Collections.Generic;
using UnityEngine;

public class CompositRootView : CompositeRoot
{
    [Header("View")]
    [SerializeField] private Castle _castlePlayer;
    [SerializeField] private Castle _castleAi;
    [SerializeField] private GoldView _goldView;
    [SerializeField] private UpgrateStatsView _upgradeStatsView;
    [Space]
    [SerializeField] private GameResult _gameResult;
    [SerializeField] private GameResultView _gameResultView;
    [SerializeField] private TriggerAnimation _triggerAnimation;

    [Header("Spawn")]
    [SerializeField] private List<ViewSprite> _images;
    [SerializeField] private ButtonUnitView _buttonUnitView;
    [SerializeField] private RadiusSpawner _radiusSpawner;

    [Header("Other System")]
    [SerializeField] private ChangerStats _changerStats;
    [SerializeField] private UnitStatsBlock _unitStatsBlockPrefab;
    [SerializeField] private SelectionSelector _selectionSelector;
    [SerializeField] private Bank _bank;

    [Header("EffectPay")]
    [SerializeField] private List<ViewSkillPay> _skillPays;
    [SerializeField] private Effect _effect;
    [SerializeField] private List<Barrack> _barracks;

    public override void Compose()
    {
        InitializeView();
        InitEffectPay();
    }

    private void InitializeView()
    {
        _buttonUnitView.Init(_radiusSpawner, _images);
        _goldView.InitGoldView(_castlePlayer.CashAccount);
        _upgradeStatsView.InitUpgrateStatsView(_changerStats, _radiusSpawner);
        _gameResult.InitGameResult(_castlePlayer, _castleAi);
        _gameResultView.InitGameResultView(_gameResult);
        _selectionSelector.InitSelectionSelector(_upgradeStatsView);
        _triggerAnimation.InitTriggerAnimation(_bank);
    }

    private void InitEffectPay()
    {
        for(int i = 0; i < _skillPays.Count; i++)
            _skillPays[i].InitViewSkillPay(_effect, _changerStats, _castlePlayer, _barracks[i]);
    }
}
