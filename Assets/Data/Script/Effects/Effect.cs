using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public float Duration => _particleSystem.main.duration;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void StartEffect()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void StopEffect()
    {
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}
