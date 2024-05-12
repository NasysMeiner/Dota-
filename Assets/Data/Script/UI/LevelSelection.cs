using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _victoryColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private GameObject _blockImage;

    private string _nameLevel;
    private Sprite _levelIcon;
    private LevelManager _levelManager;

    public void SetLevel(LevelData levelData, LevelManager levelManager)
    {
        _levelManager = levelManager;
        _nameLevel = levelData.Scene;
        _levelIcon = levelData.Icon;

        if (levelData.status == 0)
        {
            _button.interactable = false;
            _blockImage.SetActive(true);
        }
        else if(levelData.status == 1 || levelData.status == 2)
        {
            _button.interactable = true;
            _blockImage.SetActive(false);
        }

        if (levelData.status == 2)
            _buttonImage.color = _victoryColor;
        else
            _buttonImage.color = _normalColor;

        _numberText.text = _nameLevel;
        _icon.sprite = _levelIcon;
    }

    public void LoadLevel()
    {
        _levelManager.LoadLevel(_nameLevel);
    }
}
