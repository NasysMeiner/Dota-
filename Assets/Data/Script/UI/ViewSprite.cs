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
        if (_spriteRenderer != null)
            _spriteRenderer.enabled = true;
    }

    public void InActive()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.enabled = false;
    }
}
