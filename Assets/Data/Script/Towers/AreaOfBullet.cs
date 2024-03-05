using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float areaRadius;
    [SerializeField] private float damage; 

    public void ApplyAreaDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, areaRadius); //raycast dlya  2d ne podhodit napisano
        foreach (Collider2D col in colliders)
        {
            IEntity enemy = col.GetComponent<IEntity>();
            if (enemy != null)
            {
                enemy.GetDamage(damage); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEntity enemy = collision.GetComponent<IEntity>();
        if (enemy != null)
        {
            enemy.GetDamage(damage);
        }
        Destroy(gameObject);
    }
}