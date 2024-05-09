using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private Image _icon;

    private int _levelNumber;
    private Sprite _levelIcon;

    public void SetLevel(int number, Sprite icon)
    {
        _levelNumber = number;
        _levelIcon = icon;

        _numberText.text = _levelNumber.ToString();
        _icon.sprite = _levelIcon;
    }
}
