using System.Collections.Generic;
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
    [SerializeField] private AutoSpawner _autoSpawner;
    [SerializeField] private AI _ai;

    [Header("System other")]
    [SerializeField] private ChangerStats _changerStats;
    [SerializeField] private Bank _bank;
    [SerializeField] private CounterUnit _counterUnit;
    [SerializeField] private GroupCustomizerData _groupCustomizerData;
    [SerializeField] private SelectorGroupUnit _selectorGroupUnit;
    [SerializeField] private Castle _castleAi;
    [SerializeField] private TriggerZone _triggerZone;

    [Header("Skills")]
    [SerializeField] private List<SkillData> _skillData;

    public override void Compose()
    {
        LoadDataPlayer();
        LoadDataAi();
        ChangerStatsInit();
        LoadSkillData();
        InitAutoSpawner();
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

        _groupCustomizerData.InitGroupPrice(_prefabUnitStatsAi);
    }

    private void ChangerStatsInit()
    {
        _changerStats.InitChangerStats(_bank);
        _changerStats.AddUnitStat(_gameInfoPlayer.Name, _emptyUnitStatsPlayer);
        _changerStats.AddUnitStat(_gameInfoAi.Name, _emptyUnitStatsAi);
    }

    private void LoadSkillData()
    {
        foreach (var skill in _skillData)
            skill.InitData();
    }

    private void InitAutoSpawner()
    {
        _selectorGroupUnit.InitSelectorGroupUnit(_groupCustomizerData, _counterUnit, _autoSpawner, _bank, _gameInfoAi.Name);
        _triggerZone.InitTriggerZone(_gameInfoAi.Name);
        _ai.InitAI(_selectorGroupUnit, _triggerZone);
    }
}
