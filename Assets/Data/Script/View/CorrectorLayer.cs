using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CorrectorLayer : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            spriteRenderer.sortingOrder = (int)(10000 - transform.position.y * 1000);
    }
}
