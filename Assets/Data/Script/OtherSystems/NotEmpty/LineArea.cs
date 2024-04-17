using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArea : MonoBehaviour
{
    [SerializeField] private List<Unit> _liveUnitPlayer = new();
    [SerializeField] private List<Unit> _liveUnitAi = new();

    public int TotalLineWeight
    {
        get
        {
            int totalWeightUnit = 0;

            foreach (Unit unit in _liveUnitPlayer)
                totalWeightUnit -= unit.WeightUnit;

            foreach(Unit unit in _liveUnitAi)
                totalWeightUnit += unit.WeightUnit;

            return totalWeightUnit;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit))
        {
            unit.Died += OnDiedUnit;

            if (unit.Name == "Player" && !_liveUnitPlayer.Contains(unit))
                _liveUnitPlayer.Add(unit);
            else if(unit.Name == "Ai" &&  !_liveUnitAi.Contains(unit))
                _liveUnitAi.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit))
        {
            unit.Died -= OnDiedUnit;

            if (_liveUnitPlayer.Contains(unit))
                _liveUnitPlayer.Remove(unit);
            else if(_liveUnitAi.Contains(unit))
                _liveUnitAi.Remove(unit);
        }
    }

    private void OnDiedUnit(Unit unit)
    {
        if (_liveUnitPlayer.Contains(unit))
            _liveUnitPlayer.Remove(unit);
        else if (_liveUnitAi.Contains(unit))
            _liveUnitAi.Remove(unit);
    }
}
