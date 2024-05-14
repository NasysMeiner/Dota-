using System.Collections.Generic;
using UnityEngine;

public class HealPointView : MonoBehaviour
{
    private int _maxCell;
    private float _fullOneCell;
    private float _maxHealPoint;
    private List<Heart> _hearts;

    private Castle _castle;

    private void OnDisable()
    {
        _castle.HealPointChange -= OnHealPointChange;
    }

    public void Initialize(Castle castle, List<Heart> hearts)
    {
        _maxHealPoint = castle.MaxHealPointGet;
        _castle = castle;
        _hearts = hearts;

        _maxCell = _hearts.Count;
        _fullOneCell = _maxHealPoint / _maxCell;
        _castle.HealPointChange += OnHealPointChange;
    }

    private void OnHealPointChange(float currentHealPoint)
    {
        //for (int i = 0; i < _maxCell; i++)
        //{
        //    if (currentHealPoint - _fullOneCell >= 0)
        //    {
        //        _hearts[i].ChangeValue(_fullOneCell, _fullOneCell);
        //        currentHealPoint -= _fullOneCell;
        //    }
        //    else
        //    {
        //        _hearts[i].ChangeValue(currentHealPoint, _fullOneCell);
        //        currentHealPoint -= currentHealPoint;
        //    }
        //}
    }
}
