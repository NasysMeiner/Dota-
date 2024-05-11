using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ButtonStatus : MonoBehaviour
{
    [SerializeField] private Color _activeColor;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;

    private Color _standartColor;
    private Image _image;
    private Button _button;

    public bool Interectable => _button.interactable;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.interactable = false;
        _standartColor = _image.color;
    }

    public void OnButton()
    {
        _button.interactable = true;
    }

    public void ActiveButton()
    {
        _image.color = _activeColor;
    }

    public void InActiveButton()
    {
        _image.color = _standartColor;
    }
    
    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetPrice(int price)
    {
        _price.text = price.ToString();
    }
}
