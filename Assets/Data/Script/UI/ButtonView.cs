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

    public virtual void CheckPrice(WarriorData warriorData)
    {
        if (warriorData.CurrentLevel < warriorData.MaxLevel)
        {
            _textPrice.text = warriorData.Prices[warriorData.CurrentLevel].ToString();
            _textBuy.enabled = true;
            _textActive.enabled = false;
            _button.interactable = true;
        }
        else
        {
            _textPrice.enabled = false;
            _textBuy.enabled = false;
            _textActive.enabled = true;
            _button.interactable = false;
        }
    }
}
