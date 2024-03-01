using UnityEngine;

public class CompositRootData : CompositeRoot
{
    [SerializeField] private bool _isOverwrite;

    [Header("PlayerUnit")]
    [SerializeField] private DataUnitStats _prefabUnitStatsPlayer;
    [SerializeField] private DataUnitStats _emptyUnitStatsPlayer;

    [Header("PlayerAi")]
    [SerializeField] private DataUnitStats _prefabUnitStatsAi;
    [SerializeField] private DataUnitStats _emptyUnitStatsAi;

    public override void Compose()
    {
        LoadDataPlayer();
        LoadDataAi();
    }

    public void LoadDataPlayer()
    {
        if (_isOverwrite)
            for (int i = 0; i < _emptyUnitStatsPlayer.StatsPrefab.Count; i++)
                _emptyUnitStatsPlayer.StatsPrefab[i].WarriorData.LoadStat(_prefabUnitStatsPlayer.StatsPrefab[i].WarriorData);
    }

    public void LoadDataAi()
    {
        if (_isOverwrite)
            for (int i = 0; i < _emptyUnitStatsPlayer.StatsPrefab.Count; i++)
                _emptyUnitStatsAi.StatsPrefab[i].WarriorData.LoadStat(_prefabUnitStatsAi.StatsPrefab[i].WarriorData);
    }
}
