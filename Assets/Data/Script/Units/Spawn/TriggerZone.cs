using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    private int _countInZone;
    private string _name;

    public event UnityAction EnterTriggerZone;
    public event UnityAction ExitTriggetZone;

    public void InitTriggerZone(string name)
    {
        _name = name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Unit unit) && unit.Name != _name)
        {
            _countInZone++;
            CheckInZone();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && unit.Name != _name)
        {
            _countInZone--;
            CheckInZone();
        }
    }

    private void CheckInZone()
    {
        if (_countInZone > 0)
            EnterTriggerZone?.Invoke();
        else if(_countInZone == 0)
            ExitTriggetZone?.Invoke();
    }
}
