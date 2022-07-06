using TMPro;
using UnityEngine;
//shows damage value when damage is dealt
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
    private void OnDealDamage(float amount)
    {
        var text = PoolManager.Instance.Spawn(_textPrefab, transform);
        text.transform.position = transform.position;
        text.text = amount.ToString("N0");
    }
}