using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _masterVolume = 1;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _musicVolume = 1;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _effectVolume = 1;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _uiVolume = 1;

    private Bus _masterBus;
    private Bus _musicBus;
    private Bus _effectBus;
    private Bus _uiBus;

    public float MasterVolume => _masterVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            //Debug.LogError("Found more than one Audion Manager in the scene.");
        }
        Instance = this;

        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
        _effectBus = RuntimeManager.GetBus("bus:/SFX");
        _uiBus = RuntimeManager.GetBus("bus:/UI");

        _masterBus.setVolume(_masterVolume);
        _musicBus.setVolume(_musicVolume);
        _effectBus.setVolume(_effectVolume);
        _uiBus.setVolume(_uiVolume);

        Play();
    }

    public void Stop()
    {
        _masterBus.setPaused(true);
    }

    public void Play()
    {
        _masterBus.setPaused(false);
    }

    public void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
        _masterBus.setVolume(_masterVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
        _musicBus.setVolume(_musicVolume);
    }

    public void ChangeEffectVolume(float volume)
    {
        _effectVolume = volume;
        _effectBus.setVolume(_effectVolume);
    }

    public void ChangeUiVolume(float volume)
    {
        _uiVolume = volume;
        _uiBus.setVolume(_uiVolume);
    }
}
