using TMPro;
using UnityEngine;
//shows damage value when damage is dealt
public class ShowDamageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textPrefab;
    [SerializeField]
    private CharacterResourceContainer _characterResourceContainer;
    [SerializeField]
    private CharacterResource _resource;
    private void OnEnable()
    {
        _characterResourceContainer.ResourceChanged += OnResourceChange;
    }
    private void OnDisable()
    {
        _characterResourceContainer.ResourceChanged -= OnResourceChange;
    }
    private void OnResourceChange(in CharacterResourceContainer.ResourceChangeContext context)
    {
        if (context._resource != _resource || context._delta > Mathf.Epsilon)
            return;
        var text = PoolManager.Instance.Spawn(_textPrefab, transform);
        text.transform.position = transform.position;
        text.text = (-context._delta).ToString("N0");
    }
}