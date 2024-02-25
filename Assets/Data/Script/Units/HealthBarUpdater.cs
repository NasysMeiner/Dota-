using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class HealthBarUpdater : MonoBehaviour
{
    private HealthBar healthBar;
    private Unit _unit; 

    void Start()
    {
        _unit = GetComponent<Unit>();
        healthBar = GetComponentInChildren<HealthBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar component not found!");
            return;
        }

        healthBar.SetMaxHealth(_unit.MaxHealth);
        healthBar.SetHealth(_unit.HealPoint);

        _unit.HealthChanged += UpdateHealthBar;

        UpdateHealthBar(_unit.MaxHealth);
    }

    void OnDestroy()
    {
        _unit.HealthChanged -= UpdateHealthBar;
    }

    void UpdateHealthBar(float health)
    {
        healthBar.SetHealth(health);
    }
}