using UnityEngine.Events;

public class CashAccount
{
    private int _money = 0;
    private int _increase;
    private string _name;

    public int Money => _money;
    public string Name => _name;

    public event UnityAction ChangeMoney;

    public CashAccount(string name, int increase)
    {
        _name = name;
        _increase = increase;
    }

    public void MakePayment()
    {
        _money += _increase;
        ChangeMoney?.Invoke();
    }

    public bool WithdrawMoney(int money)
    {
        if (_money < money)
            return false;

        _money -= money;
        ChangeMoney?.Invoke();

        return true;
    }
}
