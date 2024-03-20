using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageColorEffectUnit : MonoBehaviour
{
    [SerializeField] private float _damageTimeSec = 1f;

    private SpriteRenderer _spriteRend;
    private Color _defaultColor;
    private Unit _unit;
    private Color _damageColor = Color.red;

    private void OnDisable()
    {
        _unit.HealthChanged -= StartEffect;
        _unit.Died -= StopEffect;
    }

    private void Start()
    {
        _unit.HealthChanged += StartEffect;
        _unit.Died += StopEffect;
        _spriteRend = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRend.color;
    }

    public void InitEffectDamage(Unit unit, Color color)
    {
        _unit = unit;
        _damageColor = color;
    }

    public void StartEffect(float health, AttackType attackType)
    {
        if(attackType != AttackType.ConstDamage)
            StartCoroutine(StartEffectCoroutine());
    }

    public void StopEffect(Unit unit)
    {
        StopAllCoroutines();
        _spriteRend.color = _defaultColor;
    }

    private IEnumerator StartEffectCoroutine()
    {
        float time = 0;
        float step = 1f / _damageTimeSec;

        while (time < _damageTimeSec)
        {
            time += Time.deltaTime;
            _spriteRend.color = Color.Lerp(_damageColor, _defaultColor, step * time);

            yield return null;
        }
    }
}
