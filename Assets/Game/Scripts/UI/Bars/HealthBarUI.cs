using UnityEngine;
public class HealthBarUI : AbstractBarUI
{
    [SerializeField]
    private Health _health;
    [SerializeField]
    private Gradient _healthGradient;
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
    protected override void UpdateValues(float factor)
    {
        base.UpdateValues(factor);
        BarImage.color = _healthGradient.Evaluate(factor);
    }
    private void UpdateHealthValues() => UpdateValues(_health.CurrentHealth, _health.MaxHealth);
    private void OnHealthValueChange(float amount) => UpdateHealthValues();
}