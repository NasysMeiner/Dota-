using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private List<ButtonView> _buttons;

    private UnitStatsBlock _unitStatsBlock;
    private string _name;
    private int _id;

    public void InitPanelStat(string name, UnitStatsBlock upgrateStatsView)
    {
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i].InitButtonView(i, this);

        _unitStatsBlock = upgrateStatsView;
        _name = name;
    }

    public void UpdateView()
    {
        WarriorData warriorData = _unitStatsBlock.GetWarriorData(_name, _id);

        _nameText.text = warriorData.name;

        foreach (ButtonView button in _buttons)
        {
            button.UpdateButton(warriorData);
        }
    }

    public void SetUnitId(int id)
    {
        _id = id;

        UpdateView();
    }

    public void UpdateStat(int idSkill)
    {
        _unitStatsBlock.UpdateStat(_name, _id, idSkill);
    }
}
