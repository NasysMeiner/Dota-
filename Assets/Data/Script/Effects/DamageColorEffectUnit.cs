using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageColorEffectUnit : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [Space]
    [SerializeField] private Color DamageColor = Color.red;
    [SerializeField] private float DamageTimeSec = 1f;

    private SpriteRenderer _spriteRend;
    private Color _defaultColor;

    private void OnDisable()
    {
        _unit.HealthChanged -= StartEffect;
    }

    private void Start()
    {
        _unit.HealthChanged += StartEffect;
        _spriteRend = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRend.color;
    }

    public void StartEffect(float health)
    {
        StartCoroutine(StartEffectCoroutine());
    }

    private IEnumerator StartEffectCoroutine()
    {
        float time = 0;
        float step = 1f / DamageTimeSec;

        while (time < DamageTimeSec)
        {
            time += Time.deltaTime;
            _spriteRend.color = Color.Lerp(DamageColor, _defaultColor, step * time);

            yield return null;
        }
    }
}
