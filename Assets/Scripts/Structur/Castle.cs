using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IStructur
{
    private int _income;

    public int Income => _income;

    private void Awake()
    {
        
    }

    public void Destruct()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void InitializeStruct(DataStructure dataStructure)
    {
        _income = dataStructure.Income;
    }

}
