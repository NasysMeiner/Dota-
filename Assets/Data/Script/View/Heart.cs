using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
    private Image _image = null;

    public void ChangeValue(float value, float fullValue)
    {
        if (_image == null)
            _image = GetComponent<Image>();

        _image.fillAmount = value / fullValue;
    }
}
