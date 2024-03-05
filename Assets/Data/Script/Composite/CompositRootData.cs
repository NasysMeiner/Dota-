using UnityEngine;

public class CompositRootData : CompositeRoot
{
    [SerializeField] private bool _isOverwrite;

    [Header("Player")]
    [SerializeField] private DataGameInfo _gameInfoPlayer;
    [SerializeField] private DataUnitStats _prefabUnitStatsPlayer;
    [SerializeField] private DataUnitStats _emptyUnitStatsPlayer;

    [Header("Ai")]
    [SerializeField] private DataGameInfo _gameInfoAi;
    [SerializeField] private DataUnitStats _prefabUnitStatsAi;
    [SerializeField] private DataUnitStats _emptyUnitStatsAi;

    [Header("System other")]
    [SerializeField] private ChangerStats _changerStats;
    [SerializeField] private Bank _bank;

    public override void Compose()
    {
        LoadDataPlayer();
        LoadDataAi();
        ChangerStatsInit();
    }

    private void LoadDataPlayer()
    {
        if (_isOverwrite)
            for (int i = 0; i < _emptyUnitStatsPlayer.StatsPrefab.Count; i++)
                _emptyUnitStatsPlayer.StatsPrefab[i].WarriorData.LoadStat(_prefabUnitStatsPlayer.StatsPrefab[i].WarriorData);
    }

    private void LoadDataAi()
    {
        if (_isOverwrite)
            for (int i = 0; i < _emptyUnitStatsPlayer.StatsPrefab.Count; i++)
                _emptyUnitStatsAi.StatsPrefab[i].WarriorData.LoadStat(_prefabUnitStatsAi.StatsPrefab[i].WarriorData);
    }

    private void ChangerStatsInit()
    {
        _changerStats.InitChangerStats(_bank);
        _changerStats.AddUnitStat(_gameInfoPlayer.Name, _emptyUnitStatsPlayer);
        _changerStats.AddUnitStat(_gameInfoAi.Name, _emptyUnitStatsAi);
    }
}
