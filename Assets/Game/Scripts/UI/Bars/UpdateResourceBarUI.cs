using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateResourceBarUI : MonoBehaviour
{
    [SerializeField]
    private CharacterResourceContainer _characterResourceContainer;
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private BarUI _barUI;
    private void OnResourceChange(in CharacterResourceContainer.ResourceChangeContext context)
    {
        if (context._resource == _resource)
            _barUI.UpdateValues(context._newValue, context._maxValue);
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
