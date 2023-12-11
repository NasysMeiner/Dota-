using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IStructur
{
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;
    private int _money;

    private List<Barracks> _barracks;

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
            Destruct();
    }

    public void InitializeCastle(DataStructure dataStructureCastle, DataGameInfo dataGameInfo, List<Barracks> structurs, DataStructure dataStructureBarracks)
    {
        InitializeStruct(dataStructureCastle, dataGameInfo.name);

        _money = dataGameInfo.StartMoney;
        _barracks = structurs;

        foreach(Barracks barracks in structurs)
        {
            barracks.InitializeStruct(dataStructureBarracks, dataGameInfo.name);
        }
    }

    public void InitializeStruct(DataStructure dataStructure, string name)
    {
        _income = dataStructure.Income;
        _name = name;
        _maxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = _maxHealPoint;
    }

}
