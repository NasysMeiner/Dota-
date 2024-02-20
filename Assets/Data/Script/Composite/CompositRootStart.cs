using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositRootStart : CompositeRoot
{
    [Header("BarrackPlayer")]
    [SerializeField] private List<Barrack> _barracksPlayer;

    [Header("BarrackAi")]
    [SerializeField] private List<Barrack> _barracksAi;

    public override void Compose()
    {
        StartGame();
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
        //_barracksPlayer[0].isEnemy = false;
        //_barracksAi[0].SpawnUnits();
        //_barracksAi[0].isEnemy = false;
    }
}
