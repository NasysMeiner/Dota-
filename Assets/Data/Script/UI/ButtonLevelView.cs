using TMPro;
using UnityEngine;

public class ButtonLevelView : ButtonView
{
    [SerializeField] private TMP_Text _currentLevel;

    public override void UpdateButton(WarriorData warriorData)
    {
        _currentLevel.text = warriorData.CurrentLevel.ToString();
        CheckPrice(warriorData);
    }
}
