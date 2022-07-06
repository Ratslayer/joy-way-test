using TMPro;
using UnityEngine;

public class TakeDamageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textPrefab;
    [SerializeField]
    private Health _health;
    private void OnEnable()
    {
        _health.TookDamage += OnDealDamage;
    }
    private void OnDisable()
    {
        _health.TookDamage -= OnDealDamage;
    }
    private void OnDealDamage(float amount, Collision collision)
    {
        var text = PoolManager.Instance.Spawn(_textPrefab, transform);
        text.transform.position = transform.position;
        text.text = amount.ToString("N0");
    }
}