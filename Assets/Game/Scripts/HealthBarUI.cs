using UnityEngine;
public class HealthBarUI : AbstractBarUI
{
    [SerializeField]
    private Health _health;

    private void OnEnable()
    {
        _health.TookDamage += OnHealthValueChange;
        _health.HealedToFull += UpdateHealthValues;
        UpdateHealthValues();
    }
    private void OnDisable()
    {
        _health.TookDamage -= OnHealthValueChange;
        _health.HealedToFull -= UpdateHealthValues;
    }
    private void UpdateHealthValues() => UpdateValues(_health.CurrentHealth, _health.MaxHealth);
    private void OnHealthValueChange(float amount, Collision collision) => UpdateHealthValues();
}