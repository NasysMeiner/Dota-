using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArea : MonoBehaviour
{
    [SerializeField] private List<Unit> _liveUnit = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit) && unit.Name == "Player" && !_liveUnit.Contains(unit))
        {
            unit.Died += OnDiedUnit;

            if (_liveUnit.Contains(unit))
                _liveUnit.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
