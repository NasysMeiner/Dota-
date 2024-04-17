using UnityEngine;
using UnityEngine.UI;

public class ButtonSkillViewRight : ButtonSkillView
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _buyColor;

    private StatViewRight _statViewRight;

    public override void InitButtonView(int id, StatView statView)
    {
        _idButton = id;
        _statViewRight = statView as StatViewRight;

        _button = GetComponent<Button>();
    }

    //public override void UpdateStat()
    //{
    //    _statViewRight.UpdateStat(_idButton - 1);
    //}

    public override void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.AllSkillList[_idButton - 1].Icon != null)
            _icon.sprite = warriorData.AllSkillList[_idButton - 1].Icon;

        if (warriorData.GetSkill(_idButton - 1) == 1)
        {
            _image.color = _buyColor;
        }
        else if (warriorData.GetSkill(_idButton - 1) == 0)
        {
            _image.color = _normalColor;
        }
        else
        {
            _image.color = Color.black;
        }
    }

    //public override void UpdateButton(WarriorData warriorData)
    //{
    //    _statViewRight.ViewText(GetTextSkill(warriorData));
    //}
}
