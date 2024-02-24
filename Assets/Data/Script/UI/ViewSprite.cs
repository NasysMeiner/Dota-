using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ViewSprite : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Active()
    {
        _spriteRenderer.enabled = true;
    }

    public void InActive()
    {
        _spriteRenderer.enabled = false;
    }
}
