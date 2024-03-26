using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : MonoBehaviour
{
    [SerializeField] protected Image _icon;
    [SerializeField] protected TMP_Text _textPrice;

    [SerializeField] protected TMP_Text _textBuy;
    [SerializeField] protected TMP_Text _textActive;

    protected int _idButton;
    protected Button _button;
    protected StatView _statView;

    public abstract void UpdateButton(WarriorData warriorData);

    public abstract void CheckPrice(WarriorData warriorData);

    public void InitButtonView(int id, StatView statView)
    {
        _idButton = id;
        _statView = statView;

        _button = GetComponent<Button>();
    }

    public void UpdateStat()
    {
        _statView.UpdateStat(_idButton - 1);
    }
}