public class UISlider : AudioSlider
{
    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.UiVolume;
    }
}
