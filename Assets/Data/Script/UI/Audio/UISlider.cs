public class UISlider : AudioSlider
{
    private void OnEnable()
    {
        if (_slider != null)
            _slider.value = AudioManager.Instance.UiVolume;
    }

    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.UiVolume;
    }
}
