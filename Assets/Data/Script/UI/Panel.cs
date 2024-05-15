using FMODUnity;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private EventReference _sound;

    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void Play()
    {
        //AudioManager.Instance.PlayOneShot(_sound, transform.position);
    }
}
