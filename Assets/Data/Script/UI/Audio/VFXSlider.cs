public class VFXSlider : AudioSlider
{
    private void OnEnable()
    {
        if (_slider != null)
            _slider.value = AudioManager.Instance.EffectVolume;
    }

    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.EffectVolume;
    }
}
