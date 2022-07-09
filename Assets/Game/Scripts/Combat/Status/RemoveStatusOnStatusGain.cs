using UnityEngine;

public class RemoveStatusOnStatusGain : MonoBehaviour
{
    [SerializeField]
    private CharacterStatusContainer _statuses;
    [SerializeField]
    private CharacterStatus _gainedStatus, _removedStatus;
    private void OnEnable()
    {
        _statuses.StatusGained += OnStatusGain;
    }
    private void OnDisable()
    {
        _statuses.StatusGained -= OnStatusGain;
    }
    private void OnStatusGain(in CharacterStatusContainer.StatusGainedContext context)
    {
        if (_gainedStatus == context._status)
            _statuses.RemoveStatus(_removedStatus);
    }
}
