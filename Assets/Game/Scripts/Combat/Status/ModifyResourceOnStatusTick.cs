using UnityEngine;
public class ModifyResourceOnStatusTick : MonoBehaviour
{
    [SerializeField]
    private CharacterResourceContainer _resources;
    [SerializeField]
    private CharacterStatusContainer _statuses;
    [SerializeField]
    private CharacterStatus _status;
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private float _valuePerSecond;
    private void OnStatusTick(in CharacterStatusContainer.StatusTickContext context)
    {
        if (context._status == _status)
            _resources.AddValue(_resource, _valuePerSecond * context._tickDuration);
    }
    private void OnEnable()
    {
        _statuses.StatusTicked += OnStatusTick;
    }
    private void OnDisable()
    {
        _statuses.StatusTicked -= OnStatusTick;
    }
}