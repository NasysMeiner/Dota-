using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour, IStructur
{
    private string _name;

    private int _income;
    private float _maxHealPoint;
    private float _healPoint;

    public string Name => _name;
    public int Income => _income;
    public float HealPoint => _healPoint;

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void InitializeStruct(DataStructure dataStructure, string name)
    {
        _income = dataStructure.Income;
        _maxHealPoint = dataStructure.MaxHealpPoint;

        _healPoint = _maxHealPoint;
    }

    public void TakeDamage(int damage)
    {
        _healPoint -= damage;

        if (_healPoint <= 0)
            Destruct();
    }
}
