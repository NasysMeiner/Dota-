using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [Header("Castle")]
    [SerializeField] private DataStructure _dataCastle;
    [SerializeField] private Castle _castle;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _castle.InitializeStruct(_dataCastle);
    }
}
