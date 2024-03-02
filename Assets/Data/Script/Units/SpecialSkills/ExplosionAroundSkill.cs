using UnityEngine;

public class ExplosionAroundSkill : Skill
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    public void InitSkill()
    {

    }

    public override void UseSkill(string name)
    {
        RaycastHit2D[] hitCollider = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero);

        foreach (RaycastHit2D c in hitCollider)
        {
            if (c.collider.gameObject.TryGetComponent(out Unit unit))
            {
                if (unit.Name != name)
                {
                    Debug.Log(unit.Name + " " + _damage);
                    unit.GetDamage(_damage);
                }
            }
        }
    }
}
