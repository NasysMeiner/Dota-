using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public float Duration => _particleSystem.main.duration;

    public void Init(float scale)
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.gameObject.transform.localScale = _particleSystem.gameObject.transform.localScale * scale;
    }

    public void StartEffect()
    {
        if(_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void StopEffect()
    {
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}
