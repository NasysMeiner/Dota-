using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateChanger : MonoBehaviour
{
    private Animator _animator;

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void OnPlayWalk()
    {
        _animator.Play("Walk");
    }

    public void OnPlayHit()
    {
        _animator.Play("Hit");
    }

    public void OnPlayDamage()
    {
        _animator.Play("Damage");
    }
}
