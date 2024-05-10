using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private Image _icon;

    private string _nameLevel;
    private Sprite _levelIcon;
    private LevelManager _levelManager;

    public void SetLevel(string name, Sprite icon, LevelManager levelManager)
    {
        _levelManager = levelManager;
        _nameLevel = name;
        _levelIcon = icon;

        _numberText.text = _nameLevel;
        _icon.sprite = _levelIcon;
    }

    public void LoadLevel()
    {
        _levelManager.LoadLevel(_nameLevel);
    }
}
