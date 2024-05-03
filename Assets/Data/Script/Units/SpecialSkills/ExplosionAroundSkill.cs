using UnityEngine;

public class ExplosionAroundSkill : Skill
{
    [SerializeField] private Effect _boomEffect;

    private Unit _unit;

    private float _attackScaling = 0.5f;
    private float _damage = 500;
    private float _radius = 2;

    private float _scaleEffect;

    public override void InitSkill(ContainerSkill container)
    {
        ContainerExplosionAroundSkill newContainer = container as ContainerExplosionAroundSkill;

        _damage = newContainer.Damage;
        _radius = newContainer.Radius;
        _attackScaling = newContainer.AttackScaling;
        TypeSkill = newContainer.TypeSkill;
        _boomEffect = Instantiate(newContainer.Effect, transform);
        _boomEffect.transform.position = transform.position;
        _boomEffect.Init(_scaleEffect);
    }

    public override void SetUnit(Unit unit)
    {
        _unit = unit;
        _scaleEffect = unit.ScaleEffect;
        //_boomEffect.Init(unit.ScaleEffect);
    }

    public override void UseSkill()
    {
        _boomEffect.StartEffect();
        RaycastHit2D[] hitCollider = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero);

        foreach (RaycastHit2D c in hitCollider)
        { 
            if (c.collider.gameObject.TryGetComponent(out Unit unit) && unit != null)
            {
                if (unit.Name != _unit.Name)
                {
                    //Debug.Log(c.collider.name);
                    unit.GetDamage(_damage + _unit.Damage * _attackScaling, AttackType.Melee);
                }
            }
        }
    }
}
