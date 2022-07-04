using UnityEngine;
[RequireComponent(typeof(Health))]
public class HideModelOnDeath : MonoBehaviour
{
    [SerializeField]
    private Transform _modelRoot;
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _health.HealedToFull += OnRevive;
        _health.ReachedZero += OnDeath;
    }
    private void OnDisable()
    {
        _health.HealedToFull -= OnRevive;
        _health.ReachedZero -= OnDeath;
    }
    private void OnDeath()
    {
        _modelRoot.gameObject.SetActive(false);
    }
    private void OnRevive()
    {
        _modelRoot.gameObject.SetActive(true);
    }
}
