using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageColorEffectCastle : MonoBehaviour
{
    [SerializeField] private Castle _castle;
    [Space]
    [SerializeField] private Color DamageColor = Color.red;
    [SerializeField] private float DamageTimeSec = 1f;

    private SpriteRenderer _spriteRend;
    private Color _defaultColor;

    private void OnDisable()
    {
        _castle.HealPointChange -= StartEffect;
    }

    private void Start()
    {
        _castle.HealPointChange += StartEffect;
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
