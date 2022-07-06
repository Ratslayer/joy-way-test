using UnityEngine;
using UnityEngine.InputSystem;

public class HealToFullOnPress : HealthComponent
{
    [SerializeField]
    private Key _fullHealKey;
    private void Update()
    {
        if (Keyboard.current[_fullHealKey].wasPressedThisFrame)
            Health.HealToFull();
    }
}
