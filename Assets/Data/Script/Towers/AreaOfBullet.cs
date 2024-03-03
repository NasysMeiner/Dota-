using UnityEngine;

public class AreaOftBullet : MonoBehaviour
{
    [SerializeField] private float areaRadius;
    [SerializeField] private float damage;

    public void ApplyAreaDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, areaRadius); //raycast dlya  2d ne podhodit napisano

        foreach (Collider2D col in colliders)
        {
            IEntity enemy = col.GetComponent<IEntity>();

            enemy?.GetDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEntity enemy = collision.GetComponent<IEntity>();

        enemy?.GetDamage(damage);

        Destroy(gameObject);
    }
}