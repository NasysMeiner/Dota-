using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdater : MonoBehaviour
{
    private HealthBar healthBar; 
    public Unit unit; 

    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar component not found!");
            return;
        }

        healthBar.SetMaxHealth(unit.MaxHealth);
        healthBar.SetHealth(unit.HealPoint);

        unit.HealthChanged += UpdateHealthBar;
    }

    void OnDestroy()
    {
        unit.HealthChanged -= UpdateHealthBar;
    }

    void UpdateHealthBar(float health)
    {
        healthBar.SetHealth(health);
    }
}