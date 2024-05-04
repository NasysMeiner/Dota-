using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private List<CashAccount> _accounts = new();

    private float _timeAddMoney = 1f;

    private bool _isWork = false;

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
        foreach (CashAccount account in _accounts)
            if (account.Name == name)
                return account.WithdrawMoney(money);

        return false;
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
