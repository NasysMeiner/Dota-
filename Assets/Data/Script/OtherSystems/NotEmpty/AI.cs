using UnityEngine;

public class AI : MonoBehaviour
{
    private SelectorGroupUnit _groupUnit;
    private TriggerZone _triggerZone;

    private bool _isSpawnDef = false;

    private TypeGroup _typeGroup = TypeGroup.AttackType;

    private void OnDisable()
    {
        _triggerZone.EnterTriggerZone -= OnSpawnTriggerZone;
        _triggerZone.ExitTriggetZone -= OnExitTriggerZone;
        _groupUnit.EndSpawn -= SpawnGroup;
    }

    public void InitAI(SelectorGroupUnit groupUnit, TriggerZone triggerZone)
    {
        _groupUnit = groupUnit;
        _triggerZone = triggerZone;

        _triggerZone.EnterTriggerZone += OnSpawnTriggerZone;
        _triggerZone.ExitTriggetZone += OnExitTriggerZone;
        _groupUnit.EndSpawn += SpawnGroup;
    }

    public void StartGame()
    {
        SpawnGroup();
    }

    private void SpawnGroup()
    {
        _groupUnit.StartBuildingGroup(_typeGroup);
    }

    private void OnSpawnTriggerZone()
    {
        if (_isSpawnDef == false)
        {
            _isSpawnDef = true;
            _typeGroup = TypeGroup.DefType;
            SpawnGroup();
        }
    }

    private void OnExitTriggerZone()
    {
        if (_isSpawnDef)
        {
            _isSpawnDef = false;
            _typeGroup = TypeGroup.AttackType;
        }
    }
}
