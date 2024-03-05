using UnityEngine;

public class AnimateChanger
{
    private Animator _animator;

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void OnPlayWalk()
    {
        if (_animator.avatar != null)
            _animator.Play("Walk");
    }

    public void OnPlayHit()
    {
        if (_animator.avatar != null)
            _animator.Play("Hit");
    }

    public void OnPlayDamage()
    {
        if (_animator.avatar != null)
            _animator.Play("Damage");
    }
}
