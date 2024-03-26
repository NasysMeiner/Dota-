using TMPro;
using UnityEngine;

public class ButtonLevelView : ButtonView
{
    [SerializeField] private TMP_Text _currentLevel;

    public override void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.CurrentLevel < warriorData.MaxLevel)
        {
            _textPrice.text = "+" + warriorData.CurrentLevel.ToString();
            _textBuy.enabled = true;
            _textActive.enabled = false;
            _button.interactable = true;
        }
        else
        {
            _textPrice.enabled = false;
            //_textPrice.text = "MAX";
            _textBuy.enabled = false;
            _textActive.enabled = true;
            _button.interactable = false;
        }
    }

    public override void UpdateButton(WarriorData warriorData)
    {
        _currentLevel.text = warriorData.CurrentLevel.ToString();
        CheckPrice(warriorData);
    }
}
