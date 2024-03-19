using UnityEngine;

public class AnimateChanger
{
    private Animator _animator;

    private string _walk = "IsWalk";
    private string _hit = "IsHit";
    private string _idle = "IsIdle";
    private string _death = "IsDeath";

    private string _currentState;

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void OnPlayWalk()
    {
        if (_animator.runtimeAnimatorController != null && _currentState != _walk)
        {
            _currentState = _walk;
            _animator.SetTrigger(_walk);
        }
    }

    public void OnPlayHit()
    {
        if (_animator.runtimeAnimatorController != null)
        {
            _currentState = _hit;
            _animator.SetTrigger(_hit);
        }
    }

    public void OnPlayIdle()
    {
        if (_animator.runtimeAnimatorController != null)
        {
            _currentState = _idle;
            _animator.SetTrigger(_idle);
        }
    }

    public void OnPlayDeath()
    {
        if (_animator.runtimeAnimatorController != null)
        {
            _currentState = _death;
            _animator.SetTrigger(_death);
        }
    }
}
