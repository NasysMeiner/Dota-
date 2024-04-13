using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelViewRight : ButtonLevelView
{
    private StatViewRight _statViewRight;

    public override void InitButtonView(int id, StatView statView)
    {
        _idButton = id;
        _statViewRight = statView as StatViewRight;

        _button = GetComponent<Button>();
    }

    public override void UpdateButton(WarriorData warriorData)
    {
        _icon.sprite = _iconsLevel[warriorData.CurrentLevel - 1];
        _statViewRight.ViewText(GetTextSkill(warriorData));
    }
}
