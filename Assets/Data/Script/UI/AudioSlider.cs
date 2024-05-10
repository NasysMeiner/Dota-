using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = AudioManager.Instance.MasterVolume;
    }

    private void OnEnable()
    {
        if (_slider != null)
            _slider.value = AudioManager.Instance.MasterVolume;
    }

    public void ChangeAllVolume()
    {
        AudioManager.Instance.ChangeMasterVolume(_slider.value);
    }

    public void ChangeMusicVolume()
    {
        AudioManager.Instance.ChangeMusicVolume(_slider.value);
    }

    public void ChangeEffectVolume()
    {
        AudioManager.Instance.ChangeEffectVolume(_slider.value);
    }

    public void ChangeUiVolume()
    {
        AudioManager.Instance.ChangeUiVolume(_slider.value);
    }
}
