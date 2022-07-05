using System;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
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
    public void HealToFull()
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