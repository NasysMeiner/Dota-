using System.Collections.Generic;
using UnityEngine;

public class ViewSkillPay : MonoBehaviour
{
    private Effect _effect;
    private ChangerStats _changerStats;
    private string _name;
    private CashAccount _cashAccount;
    private List<int> _units = new();

    private bool _isActive = false;

    private void OnDisable()
    {
        _cashAccount.ChangeMoney -= OnChangeMoney;
    }

    public void InitViewSkillPay(Effect effect, ChangerStats changerStats, Castle castle, Barrack barrack)
    {
        _effect = Instantiate(effect, transform);
        _effect.Init(1);
        _effect.StopEffect();
        _changerStats = changerStats;
        _name = castle.Name;
        _cashAccount = castle.CashAccount;

        for(int i = barrack.StartIdUnit; i < barrack.StartIdUnit + 2;  i++)
            _units.Add(i);

        _cashAccount.ChangeMoney += OnChangeMoney;
    }

    public void OnChangeMoney()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_changerStats.CheckUnlock(_name, _units[i]))
            {
                PlayEffect();

                return;
            }
        }

        if(_isActive)
            StopEffect();

    }

    public void Destruct()
    {
        _cashAccount.ChangeMoney -= OnChangeMoney;
        StopEffect();
    }

    public void Return()
    {
        _cashAccount.ChangeMoney += OnChangeMoney;
        PlayEffect();
    }

    public void PlayEffect()
    {
        if(_isActive == false)
        {
            if(_effect != null)
                _effect.StartEffect();

            _isActive = true;
        }
    }

    public void StopEffect()
    {
        if (_isActive)
        {
            if (_effect != null)
                _effect.StopEffect();

            _isActive = false;
        }
    }
}
