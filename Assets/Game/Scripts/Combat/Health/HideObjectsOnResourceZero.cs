using UnityEngine;
public class HideObjectsOnResourceZero : MonoBehaviour
{
    [SerializeField]
    private CharacterResourceContainer _resourceContainer;
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private Transform[] _transforms;
    [SerializeField]
    private CanvasGroup[] _groups;
    private void OnEnable()
    {
        _resourceContainer.ResourceChanged += OnResourceChange;
    }
    private void OnDisable()
    {
        _resourceContainer.ResourceChanged -= OnResourceChange;
    }
    private void OnResourceChange(in CharacterResourceContainer.ResourceChangeContext context)
    {
        if (context._resource != _resource)
            return;
        var visible = context._newValue > Mathf.Epsilon;
        foreach (var obj in _transforms)
            obj.gameObject.SetActive(visible);
        var alpha = visible ? 1 : 0f;
        foreach (var group in _groups)
            group.alpha = alpha;
    }
}
