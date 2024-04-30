using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ConstantAreaDamage : Skill
{
    [SerializeField] private bool _isDrawRadius = false;
    [SerializeField] private Effect _boomEffect;

    private Unit _unit;
    private CircleCollider2D _circleCollider;

    private float _attackScaling = 0;
    private float _damage = 20;
    private float _time = 0.2f;
    private float _radius = 2;

    private float _scaleEffect;

    private List<Unit> _unitList = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && unit.Name != _unit.Name)
        {
            _unitList.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit) && _unitList.Contains(unit))
        {
            _unitList.Remove(unit);
        }
    }

    public override void InitSkill(ContainerSkill container)
    {
        _isActive = false;

        ConstantAreaDamageSkill data = container as ConstantAreaDamageSkill;

        _attackScaling = data.AttackScaling;
        _damage = data.Damage;
        _time = data.Time;
        _radius = data.Radius;
        TypeSkill = data.TypeSkill;
        _boomEffect = Instantiate(data.Effect, transform);
        _boomEffect.transform.position = transform.position;
        _boomEffect.Init(_scaleEffect);
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
        _circleCollider.radius = _radius;
    }

    public override void SetUnit(Unit unit)
    {
        _unit = unit;
        _scaleEffect = unit.ScaleEffect;
    }

    public override void UseSkill()
    {
        _isActive = true;
        StartCoroutine(MakeDamageOfArea());
        _boomEffect.StartEffect();
    }

    public override void StopSkill()
    {
        base.StopSkill();
        _boomEffect.StopEffect();
    }

    private IEnumerator MakeDamageOfArea()
    {
        while (_isActive)
        {
            foreach (Unit unit in _unitList)
                if(unit != null)
                {
                    unit.GetDamage(_damage + _unit.Damage * _attackScaling, AttackType.ConstDamage);
                    //Debug.Log(unit.Name + " " + (_damage + _unit.Damage * _attackScaling));
                }

            yield return new WaitForSeconds(_time);
        }
    }

    private void OnDrawGizmos()
    {
        if (_isDrawRadius)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _radius / 2);
        }
    }
}
