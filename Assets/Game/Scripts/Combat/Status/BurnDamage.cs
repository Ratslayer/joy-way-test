using UnityEngine;
//deals fire damage based on elapsed burn duration
//deals damage in 1 second intervals
[RequireComponent(typeof(WetHotStatus))]
public class BurnDamage : MonoBehaviour
{
    [SerializeField]
    private CharacterResourceContainer _resourceContainer;
    [SerializeField]
    private CharacterResource _targetResource;
    [SerializeField]
    private float _damagePerSecond = 5f;
    private WetHotStatus _status;
    private float _elapsedBurnDuration;
    private void Awake()
    {
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
            _resourceContainer.AddValue(_targetResource, -_damagePerSecond);
            _elapsedBurnDuration -= 1f;
        }
    }
}