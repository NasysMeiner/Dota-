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

    public void ChangeVolume()
    {
        AudioManager.Instance.ChangeVolume(_slider.value);
    }
}
