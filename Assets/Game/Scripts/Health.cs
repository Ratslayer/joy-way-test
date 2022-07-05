using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Health : MonoBehaviour
{
    [SerializeField]
    private Key _fullHealKey;
    [SerializeField]
    private float _maxHealth = 1000;
    private float _currentHealth;
    public event Action ReachedZero;
    public event Action HealedToFull;
    public event Action<float> TookDamage;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    private void Awake()
    {
        HealToFull();
    }
    private void Update()
    {
        if (Keyboard.current[_fullHealKey].wasPressedThisFrame)
            HealToFull();
    }
    private void HealToFull()
    {
        _currentHealth = _maxHealth;
        HealedToFull?.Invoke();
    }
    public void TakeDamage(float amount)
    {
        if (_currentHealth > 0 && amount > 0)
        {
            _currentHealth -= amount;
            TookDamage?.Invoke(amount);
            if (_currentHealth <= 0)
                ReachedZero?.Invoke();
        }
    }
}