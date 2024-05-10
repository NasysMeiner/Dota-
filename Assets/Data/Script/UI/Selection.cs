using UnityEngine;

public class Selection : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.gameObject.SetActive(false);
    }

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
