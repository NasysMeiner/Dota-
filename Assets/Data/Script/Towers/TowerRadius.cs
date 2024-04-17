using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerRadius : MonoBehaviour
{
    private Tower _tower;

    private CircleCollider2D _circleCollider;

    private float _timeCheck = 0.1f;

    private float _time;

    private void FixedUpdate()
    {
        if (_tower.CurrentTarget == null && _timeCheck <= _time)
            CheckTarget();
        else if (_tower.CurrentTarget == null)
            _time += Time.fixedDeltaTime;
        else
            _time = 0;
    }

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
        _time = _timeCheck;

        _circleCollider = GetComponent<CircleCollider2D>();
        _tower = tower;
        _circleCollider.radius = attackRange;
    }

    private void CheckTarget()
    {
        RaycastHit2D[] hitCollider = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.zero);

        foreach (RaycastHit2D c in hitCollider)
        {
            if (c.collider.gameObject.TryGetComponent(out Unit unit) && unit.Name != _tower.Name && _tower.IsAlive)
            {
                _tower.ChangeTarget(unit);
            }
        }

        _time = 0;
    }
}
