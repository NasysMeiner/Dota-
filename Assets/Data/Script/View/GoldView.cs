using TMPro;
using UnityEngine;

public class GoldView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private CashAccount _account;

    private void OnDisable()
    {
        _account.ChangeMoney -= ChangeText;
    }

    public void InitGoldView(CashAccount account)
    {
        _account = account;
        _account.ChangeMoney += ChangeText;
        ChangeText();
    }

    private void ChangeText()
    {
        _text.text = _account.Money.ToString();
    }
}
