using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Range(0.0f, 1.0f)]
    public float masterVolume = 1;

    private Bus masterBus;

    private void Awake()
    {
        if (Instance == null)
        {
            //Debug.LogError("Found more than one Audion Manager in the scene.");
        }
        Instance = this;

        masterBus = RuntimeManager.GetBus("bus:/");
        masterBus.setVolume(masterVolume);
    }

    public void PlayMusic()
    {
        
    }
}
