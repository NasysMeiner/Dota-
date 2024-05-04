using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError("Found more than one Audion Manager in the scene.");
        }
        Instance = this;
    }
}
