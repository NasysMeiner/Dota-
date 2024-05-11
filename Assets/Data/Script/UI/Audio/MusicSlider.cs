public class MusicSlider : AudioSlider
{
    private void OnEnable()
    {
        if (_slider != null)
            _slider.value = AudioManager.Instance.MusicVolume;
    }

    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.MusicVolume;
    }
}
