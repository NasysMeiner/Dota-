using System.Collections.Generic;
using UnityEngine;

public class CompositRootStart : CompositeRoot
{
    [Header("BarrackPlayer")]
    [SerializeField] private List<Barrack> _barracksPlayer;

    [Header("BarrackAi")]
    [SerializeField] private List<Barrack> _barracksAi;

    [Header("Bank")]
    [SerializeField] private Bank _bank;

    public override void Compose()
    {
        StartGame();
    }

    private void StartGame()
    {
        //foreach (var barrack in _barracksPlayer)
        //{
        //    barrack.SpawnUnits();
        //}

        //foreach (var barrack in _barracksAi)
        //{
        //    barrack.SpawnUnits();
        //}

        _bank.StartBank();

        //_barracksPlayer[0].SpawnUnits();
        //_barracksPlayer[0].isEnemy = false;
        //_barracksAi[0].SpawnUnits();
        //_barracksAi[0].isEnemy = false;
    }
}
