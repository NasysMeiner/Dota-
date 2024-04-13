using UnityEngine;
using UnityEngine.UI;

public class ButtonSkillViewRight : ButtonSkillView
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _buyColor;

    public override void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.AllSkillList[_idButton - 1].Icon != null)
            _icon.sprite = warriorData.AllSkillList[_idButton - 1].Icon;

        Debug.Log(warriorData);

        if (warriorData.GetSkill(_idButton - 1) == 1)
        {
            Debug.Log("Buy");
            _image.color = _buyColor;
        }
        else if (warriorData.GetSkill(_idButton - 1) == 0)
        {
            Debug.Log("NotBuy");
            _image.color = _normalColor;
        }
        else
        {
            Debug.Log("Tilt");
            _image.color = Color.black;
        }
    }
}
