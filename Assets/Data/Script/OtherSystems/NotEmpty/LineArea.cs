using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArea : MonoBehaviour
{
    private List<IUnit> _liveUnit = new();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit) && unit.Name == "Player" && !_liveUnit.Contains(unit))
        {
            unit.Died += OnDiedUnit;
            _liveUnit.Add(unit);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit) && unit.Name == "Player")
        {
            unit.Died -= OnDiedUnit;

            if (_liveUnit.Contains(unit))
                _liveUnit.Remove(unit);
        }
    }

    private void OnDiedUnit(Unit unit)
    {
        if (_liveUnit.Contains(unit))
            _liveUnit.Remove(unit);
    }
}
