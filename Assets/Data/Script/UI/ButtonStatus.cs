using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonStatus : MonoBehaviour
{
    [SerializeField] private Color _activeColor;

    private Color _standartColor;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _standartColor = _image.color;
    }

    public void ActiveButton()
    {
        _image.color = _activeColor;
    }

    public void InActiveButton()
    {
        _image.color = _standartColor;
    }
}
