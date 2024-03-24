using UnityEngine;

public class ButtonSkillView : ButtonView
{
    public override void UpdateButton(WarriorData warriorData)
    {
        CheckPrice(warriorData);
    }

    public override void CheckPrice(WarriorData warriorData)
    {
        Debug.Log(warriorData.GetSkill(_idButton - 1) + " " + this.name);

        if (warriorData.GetSkill(_idButton - 1) == 1)
        {
            //_textPrice.text = "Off";
            _textPrice.enabled = false;
            _textBuy.enabled = false;
            _textActive.enabled = true;
            _button.interactable = false;
        }
        else if(warriorData.GetSkill(_idButton - 1) == 0)
        {
            _textPrice.text = warriorData.AllSkillList[_idButton - 1].PriceSkill.ToString();
            //_textPrice.text = "On";
            _textBuy.enabled = true;
            _textActive.enabled = false;
            _button.interactable = true;
        }
        else
        {
            _textPrice.text = "Nooo";
            _textBuy.enabled = false;
            _textActive.enabled = false;
            _button.interactable = false;
        }
    }
}
