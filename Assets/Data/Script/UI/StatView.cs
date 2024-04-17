using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] protected List<ButtonView> _buttons;

    protected UnitStatsBlock _unitStatsBlock;
    protected string _name;
    protected int _id;

    public void InitPanelStat(string name, UnitStatsBlock upgrateStatsView)
    {
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i].InitButtonView(i, this);

        _unitStatsBlock = upgrateStatsView;
        _name = name;
    }

    public virtual void UpdateView()
    {
        WarriorData warriorData = _unitStatsBlock.GetWarriorData(_name, _id);

        if(_icon != null)
            _icon.sprite = warriorData.Icon;

        if(_nameText != null)
            _nameText.text = warriorData.name;

        foreach (ButtonView button in _buttons)
        {
            button.UpdateButton(warriorData);
        }
    }

    public void SetUnitId(int id)
    {
        _id = id;

        //Debug.Log(_id);

        UpdateView();
    }

    public void UpdateStat(int idSkill)
    {
        _unitStatsBlock.UpdateStat(_name, _id, idSkill);
    }
}
