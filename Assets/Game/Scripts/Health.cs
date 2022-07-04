using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Health : MonoBehaviour
{
    [SerializeField]
    private Key _fullHealKey;
    [SerializeField]
    private int _maxHealth = 1000;
    private int _currentHealth;
    public event Action ReachedZero;
    public event Action HealedToFull;
    public event Action<int> TookDamage;
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
    public void TakeDamage(int amount)
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