using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Range(0.0f, 1.0f)]
    public float masterVolume = 1;

    private Bus masterBus;

    public float MasterVolume => masterVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            //Debug.LogError("Found more than one Audion Manager in the scene.");
        }
        Instance = this;

        masterBus = RuntimeManager.GetBus("bus:/");
        masterBus.setVolume(masterVolume);
        Play();
    }

    public void Stop()
    {
        masterBus.setPaused(true);
    }

    public void Play()
    {
        masterBus.setPaused(false);
    }

    public void ChangeVolume(float volume)
    {
        masterVolume = volume;
        masterBus.setVolume(masterVolume);
    }
}
