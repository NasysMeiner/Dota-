using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositRootView : CompositeRoot
{
    [Header("View")]
    [SerializeField] private Castle _castlePlayer;
    [SerializeField] private List<Heart> _hearts;
    [SerializeField] private HealPointView _healPointView;

    [Header("Spawn")]
    [SerializeField] private ButtonUnitView _buttonUnitView;
    [SerializeField] private RadiusSpawner _radiusSpawner;

    public override void Compose()
    {
        InitializeView();
    }

    private void InitializeView()
    {
        _healPointView.Initialize(_castlePlayer, _hearts);
        _buttonUnitView.Init(_radiusSpawner);
    }
}
