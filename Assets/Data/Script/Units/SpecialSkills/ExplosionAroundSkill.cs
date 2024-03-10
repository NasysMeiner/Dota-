using UnityEngine;

public class ExplosionAroundSkill : Skill
{
    private Unit _unit;

    private float _attackScaling = 0.5f;
    private float _damage = 500;
    private float _radius = 2;

    public override void InitSkill(ContainerSkill container)
    {
        ContainerExplosionAroundSkill newContainer = container as ContainerExplosionAroundSkill;

        _damage = newContainer.Damage;
        _radius = newContainer.Radius;
        _attackScaling = newContainer.AttackScaling;
        TypeSkill = newContainer.TypeSkill;
    }

    public override void SetUnit(Unit unit)
    {
        _unit = unit;
    }

    public override void UseSkill()
    {
        RaycastHit2D[] hitCollider = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero);

        foreach (RaycastHit2D c in hitCollider)
        {
            if (c.collider.gameObject.TryGetComponent(out Unit unit) && unit != null)
            {
                if (unit.Name != _unit.Name)
                {
                    unit.GetDamage(_damage + _unit.Damage * _attackScaling, AttackType.Melee);
                }
            }
        }
    }
}
