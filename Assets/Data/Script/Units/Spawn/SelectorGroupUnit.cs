using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectorGroupUnit : MonoBehaviour
{
    public float TimeNextGroup = 5f;
    public int CounterGroup = 0;
    public bool IsCycle = false;

    private List<Group> _groups = new();
    private CounterUnit _unitCounter;
    private AutoSpawner _autoSpawner;
    private Bank _bank;
    private string _name;

    private bool _isStopPay = false;

    public event UnityAction EndSpawn;

    public void InitSelectorGroupUnit(GroupCustomizerData groupCustomizerData, CounterUnit counterUnit, AutoSpawner autoSpawner, Bank bank, string name)
    {
        _groups = groupCustomizerData.GroupUnits;
        _unitCounter = counterUnit;
        _autoSpawner = autoSpawner;
        _bank = bank;
        _name = name;
    }

    public void StartBuildingGroup(TypeGroup typeGroup)
    {
        if (typeGroup == TypeGroup.DefType)
            _isStopPay = true;
        else
            _isStopPay = false;

        StartCoroutine(MakePurchase(GetGroup(typeGroup), typeGroup));
    }

    public IEnumerator MakePurchase(Group group, TypeGroup typeGroup = TypeGroup.AttackType)
    {
        bool pay = false;

        while (true)
        {
            if (_bank.Pay(group.PriceGroup, _name))
            {
                pay = true;

                break;
            }

            if (_isStopPay && typeGroup != TypeGroup.DefType)
                break;

            yield return null;
        }

        if (pay)
        {
            _autoSpawner.EndSpawn += OnEndSpawn;
            _autoSpawner.StartSpawn(group, _unitCounter.GetDangerLine(typeGroup));
        }
    }

    private Group GetGroup(TypeGroup typeGroup)
    {
        int value = 0;

        foreach (var group in _groups)
            if (group.TypeGroup == typeGroup)
                value++;

        value = Random.Range(1, value + 1);

        int ind = 0;

        foreach (var group in _groups)
        {
            if (group.TypeGroup == typeGroup)
            {
                ind++;

                if (ind == value)
                    return group;
            }
        }

        Debug.Log("Krivo " + value);

        return null;
    }

    private void OnEndSpawn()
    {
        _autoSpawner.EndSpawn -= OnEndSpawn;
        EndSpawn?.Invoke();
    }
}

[System.Serializable]
public enum TypeGroup
{
    AttackType,
    DefType,
}
