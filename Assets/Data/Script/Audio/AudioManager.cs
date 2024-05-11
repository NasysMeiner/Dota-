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
    public float MusicVolume => _musicVolume;
    public float EffectVolume => _effectVolume;
    public float UiVolume => _uiVolume;

    private void Awake()
    {
        Instance = this;

        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
        _effectBus = RuntimeManager.GetBus("bus:/SFX");
        _uiBus = RuntimeManager.GetBus("bus:/UI");

        _masterBus.setPaused(true);
        _musicBus.setPaused(true);
        _effectBus.setPaused(true);
        _uiBus.setPaused(true);

        _masterBus.setVolume(_masterVolume);
        _musicBus.setVolume(_musicVolume);
        _effectBus.setVolume(_effectVolume);
        _uiBus.setVolume(_uiVolume);
    }

    private void Start()
    {
        LoadAudioData();
        Play();
    }

    public void Stop()
    {
        _masterBus.setPaused(true);
        _musicBus.setPaused(true);
        _effectBus.setPaused(true);
        _uiBus.setPaused(true);
    }

    public void Play()
    {
        _masterBus.setPaused(false);
        _musicBus.setPaused(false);
        _effectBus.setPaused(false);
        _uiBus.setPaused(false);
    }

    public void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
        _masterBus.setVolume(_masterVolume);
        SaveDataAudio();
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
        _musicBus.setVolume(_musicVolume);
        SaveDataAudio();
    }

    public void ChangeEffectVolume(float volume)
    {
        _effectVolume = volume;
        _effectBus.setVolume(_effectVolume);
        SaveDataAudio();
    }

    public void ChangeUiVolume(float volume)
    {
        _uiVolume = volume;
        _uiBus.setVolume(_uiVolume);
        SaveDataAudio();
    }

    public void LoadAudioData()
    {
        if(Repository.TryGetData(out AudioData audioData))
        {
            _masterVolume = audioData.MasterVolume;
            _musicVolume = audioData.MusicVolume;
            _effectVolume = audioData.EffectVolume;
            _uiVolume = audioData.UiVolume;

            _masterBus.setVolume(_masterVolume);
            _musicBus.setVolume(_musicVolume);
            _effectBus.setVolume(_effectVolume);
            _uiBus.setVolume(_uiVolume);
        }
    }

    public void SaveDataAudio()
    {
        AudioData audioData = new(_masterVolume, _musicVolume, _effectVolume, _uiVolume);

        Repository.SetData(audioData);
    }
}

[System.Serializable]
public class AudioData
{
    public float MasterVolume = 1;
    public float MusicVolume = 1;
    public float EffectVolume = 1;
    public float UiVolume = 1;

    public AudioData(float masterVolume, float musicVolume, float effectVolume, float uiVolume)
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        EffectVolume = effectVolume;
        UiVolume = uiVolume;
    }
}
