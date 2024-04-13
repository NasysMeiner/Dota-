using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonLevelView : ButtonView
{
    [SerializeField] protected List<Sprite> _iconsLevel;

    public override void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.CurrentLevel < warriorData.MaxLevel)
        {
            if (warriorData.CurrentLevel < _iconsLevel.Count && warriorData.CurrentLevel > 0)
                _icon.sprite = _iconsLevel[warriorData.CurrentLevel - 1];
            else
                _icon.sprite = _iconsLevel[_iconsLevel.Count - 1];

            _textPrice.text = warriorData.GetPriceLevel().ToString();
            _textBuy.text = warriorData.GetPriceLevel().ToString();
            _textPrice.enabled = true;
            _textBuy.enabled = true;
            _textActive.enabled = false;
            _button.interactable = true;
        }
        else
        {
            _textBuy.enabled = false;
            _textPrice.enabled = false;
            _textActive.enabled = true;
            _textActive.text = "MAX";
            _button.interactable = false;
        }
    }

    public override void UpdateButton(WarriorData warriorData)
    {
        _icon.sprite = _iconsLevel[warriorData.CurrentLevel - 1];
        CheckPrice(warriorData);
    }
}
