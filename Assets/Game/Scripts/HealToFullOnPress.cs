using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
public class HealToFullOnPress : MonoBehaviour
{
    [SerializeField]
    private Key _fullHealKey;
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void Update()
    {
        if (Keyboard.current[_fullHealKey].wasPressedThisFrame)
            _health.HealToFull();
    }
}
