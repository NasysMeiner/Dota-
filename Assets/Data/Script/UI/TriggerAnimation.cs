using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnimation : MonoBehaviour
{
    private Bank _bank;
    private Animator _animator;

    private void OnDisable()
    {
        _bank.NoMoney -= PlayAnim;
    }

    public void InitTriggerAnimation(Bank bank)
    {
        _bank = bank;

        _animator = GetComponent<Animator>();
        _bank.NoMoney += PlayAnim;
    }

    private void PlayAnim()
    {
        _animator.SetTrigger("Play");
    }
}
