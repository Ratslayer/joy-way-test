using UnityEngine;
//deals fire damage based on elapsed burn duration
//deals damage in 1 second intervals
[RequireComponent(typeof(WetHotStatus), typeof(Health))]
public class BurnDamage : MonoBehaviour
{
    [SerializeField]
    private float _damagePerSecond = 5f;
    private Health _health;
    private WetHotStatus _status;
    private float _elapsedBurnDuration;
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
    private void DealBurnDamage(float tick)
    {
        _elapsedBurnDuration += tick;
        if (_elapsedBurnDuration > 1f)
        {
            _health.TakeDamage(_damagePerSecond);
            _elapsedBurnDuration -= 1f;
        }
    }
}