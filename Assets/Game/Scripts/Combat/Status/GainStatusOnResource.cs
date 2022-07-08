using UnityEngine;

public class GainStatusOnResource : MonoBehaviour
{
    [SerializeField]
    private CharacterResourceContainer _characterResourceContainer;
    [SerializeField]
    private CharacterStatusContainer _characterStatusContainer;
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private CharacterStatus _status;
    private void OnResourceChange(in CharacterResourceContainer.ResourceChangeContext context)
    {
        if (context._resource != _resource)
            return;
        if (context._newValue > Mathf.Epsilon)
            _characterStatusContainer.AddStatus(_status);
        else _characterStatusContainer.RemoveStatus(_status);
    }
    private void OnEnable()
    {
        _characterResourceContainer.ResourceChanged += OnResourceChange;
    }
    private void OnDisable()
    {
        _characterResourceContainer.ResourceChanged -= OnResourceChange;
    }
}