using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnimation : MonoBehaviour
{
    private Bank _bank;
    private Animator _animator;
    private Animation _animation;

    private bool _isPlaying = false;

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
        if (_isPlaying == false)
        {
            _isPlaying = true;
            StartCoroutine(StartAnim());
        }
    }

    private IEnumerator StartAnim()
    {
        Debug.Log(_animator.GetCurrentAnimatorClipInfo(0).Length);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);

        _isPlaying = false;
    }
}
