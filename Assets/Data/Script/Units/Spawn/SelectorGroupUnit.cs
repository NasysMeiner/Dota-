using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorGroupUnit : MonoBehaviour
{
    public float TimeNextGroup = 5f;
    public int CounterGroup = 0;
    public bool IsCycle = false;

    private List<Group> _groups = new();
    private CounterUnit _unitCounter;
    private AutoSpawner _autoSpawner;

    public void InitSelectorGroupUnit(GroupCustomizerData groupCustomizerData, CounterUnit counterUnit, AutoSpawner autoSpawner)
    {
        _groups = groupCustomizerData.GroupUnits;
        _unitCounter = counterUnit;
        _autoSpawner = autoSpawner;
    }

    public void StartBuildingGroup()
    {
        if (CounterGroup < _groups.Count)
        {
            _autoSpawner.EndSpawn += OnEndSpawn;

            _autoSpawner.StartSpawn(_groups[CounterGroup], _unitCounter.GetDangerLine());

            CounterGroup++;
        }
    }

    public IEnumerator WaitForNextGroup(float time)
    {
        yield return new WaitForSeconds(time);

        if(CounterGroup < _groups.Count)
            StartBuildingGroup();
        else
            CounterGroup = 0;

        if(CounterGroup == 0 && IsCycle)
            StartBuildingGroup();
    }

    private void OnEndSpawn()
    {
        _autoSpawner.EndSpawn -= OnEndSpawn;

        StartCoroutine(WaitForNextGroup(TimeNextGroup));
    }
}
