using System.Collections;
using UnityEngine;

public class DamageColorEffectBarrack : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [Space]
    [SerializeField] private Color DamageColor = Color.red;
    [SerializeField] private float DamageTimeSec = 1f;

    private SpriteRenderer _spriteRend;
    private Color _defaultColor;

    private void OnDisable()
    {
        _barrack.HealthChange -= StartEffect;
    }

    private void Start()
    {
        _barrack.HealthChange += StartEffect;
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
