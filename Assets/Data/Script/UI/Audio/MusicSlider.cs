public class MusicSlider : AudioSlider
{
    protected override void InitSlider()
    {
        _slider.value = AudioManager.Instance.MusicVolume;
    }
}
