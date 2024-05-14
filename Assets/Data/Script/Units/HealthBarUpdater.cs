using UnityEngine;

public class HealthBarUpdater : MonoBehaviour
{
    private HealthBar healthBar;
    private Unit _unit;

    public void InitHealthBar(Unit unit)
    {
        _unit = unit;
        healthBar = GetComponentInChildren<HealthBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar component not found!");

            return;
        }

        healthBar.SetMaxHealth(_unit.MaxHealthPointGet);
        healthBar.SetHealth(_unit.HealPoint);

        _unit.HealthChanged += UpdateHealthBar;

        UpdateHealthBar(_unit.MaxHealthPointGet, AttackType.ConstDamage);
    }

    void OnDestroy()
    {
        _unit.HealthChanged -= UpdateHealthBar;
    }

    void UpdateHealthBar(float health, AttackType attackType)
    {
        healthBar.SetHealth(health);
    }
}