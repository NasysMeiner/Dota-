public class VFXSlider : AudioSlider
{
    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.EffectVolume;
    }
}
