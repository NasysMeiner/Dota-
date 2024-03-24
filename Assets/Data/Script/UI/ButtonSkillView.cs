public class ButtonSkillView : ButtonView
{
    public override void UpdateButton(WarriorData warriorData)
    {
        CheckPrice(warriorData);
    }

    public override void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.AllSkillList[_idButton - 1].IsPurchased)
        {
            _textPrice.enabled = false;
            _textBuy.enabled = false;
            _textActive.enabled = true;
            _button.interactable = false;
        }
        else
        {
            _textPrice.text = warriorData.AllSkillList[_idButton - 1].PriceSkill.ToString();
            _textBuy.enabled = true;
            _textActive.enabled = false;
            _button.interactable = true;
        }
    }
}
