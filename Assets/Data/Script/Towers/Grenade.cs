using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private float _damageReductionPercentage = 0.5f;
    private bool _isDrawRadius = false;

    protected override void MakeDamage(IEntity enemy)
    {
        base.MakeDamage(enemy);

        RaycastHit2D[] hitCollider = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero);
        _isDrawRadius = true;

        foreach (RaycastHit2D c in hitCollider)
        {
            if (c.collider.gameObject.TryGetComponent(out Unit unit))
            {
                if (unit.Name != _name)
                {
                    //Debug.Log(unit.Name + " " + _damage * _damageReductionPercentage);
                    unit.GetDamage(_damage * _damageReductionPercentage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_isDrawRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}