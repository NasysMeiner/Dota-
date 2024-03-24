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

    public abstract void UpdateButton(WarriorData warriorData);

    public void InitButtonView(int id)
    {
        _idButton = id;
        _button = GetComponent<Button>();
    }

    public abstract void CheckPrice(WarriorData warriorData);
}
