using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerRadius : MonoBehaviour
{
    private Tower _tower;

    private CircleCollider2D _circleCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && unit.Name != _tower.Name && _tower.IsAlive)
        {
            if (_tower.CurrentTarget == null)
            {
                _tower.ChangeTarget(unit);
            }
            else
            {
                float currentDistance = Mathf.Abs((_tower.CurrentTarget.Position - transform.position).magnitude);
                float newDistance = Mathf.Abs((unit.Position - transform.position).magnitude);

                if (newDistance < currentDistance)
                    _tower.ChangeTarget(unit);
            }
        }
    }

    public void InitTowerRadius(Tower tower, float attackRange)
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _tower = tower;
        _circleCollider.radius = attackRange;
    }
}
