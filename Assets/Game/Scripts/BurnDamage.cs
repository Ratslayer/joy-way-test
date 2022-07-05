using UnityEngine;

[RequireComponent(typeof(WetHotStatus), typeof(Health))]
public class BurnDamage : MonoBehaviour
{
    [SerializeField]
    private float _damagePerSecond = 5f;
    private Health _health;
    private WetHotStatus _status;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _status = GetComponent<WetHotStatus>();
    }
    private void OnEnable()
    {
        _status.BurnedForDuration += DealBurnDamage;
    }
    private void OnDisable()
    {
        _status.BurnedForDuration -= DealBurnDamage;
    }
    private void DealBurnDamage(float duration) => _health.TakeDamage(duration * _damagePerSecond);
}
