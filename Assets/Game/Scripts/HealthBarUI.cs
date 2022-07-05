using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Health _health;
    [SerializeField]
    private Image _barImage;
    [SerializeField]
    private TextMeshProUGUI _text;
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
    private void UpdateHealthValues()
    {
        _text.text = _health.CurrentHealth.ToString("N0");
        _barImage.fillAmount = _health.CurrentHealth / _health.MaxHealth;
    }
    private void OnHealthValueChange(float amount) => UpdateHealthValues();
}