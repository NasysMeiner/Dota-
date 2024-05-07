using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    private List<CashAccount> _accounts = new();

    private float _timeAddMoney = 1f;

    private bool _isWork = false;

    public event UnityAction NoMoney;

    public void InitBank(float timeAddMoney)
    {
        _timeAddMoney = timeAddMoney;
    }

    public void StartBank()
    {
        _isWork = true;
        StartCoroutine(AddMoney());
    }

    public void AddCashAccount(CashAccount cashAccount)
    {
        if (_accounts.Contains(cashAccount) == false)
            _accounts.Add(cashAccount);
    }

    public bool Pay(int money, string name)
    {
        bool isPurchase = false;

        foreach (CashAccount account in _accounts)
            if (account.Name == name)
                isPurchase = account.WithdrawMoney(money);

        if (!isPurchase)
            NoMoney?.Invoke();

        return isPurchase;
    }

    private IEnumerator AddMoney()
    {
        while (_isWork)
        {
            foreach (CashAccount account in _accounts)
                account.MakePayment();

            yield return new WaitForSeconds(_timeAddMoney);
        }
    }
}
