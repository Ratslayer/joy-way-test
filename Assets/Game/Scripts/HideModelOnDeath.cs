using UnityEngine;
[RequireComponent(typeof(Health))]
public class HideModelOnDeath : MonoBehaviour
{
    [SerializeField]
    private Transform[] _objectsToHide;
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
    private void SetVisibility(bool active)
    {
        foreach(var obj in _objectsToHide)
            obj.gameObject.SetActive(active);
    }
    private void OnDeath()
    {
        SetVisibility(false);
    }
    private void OnRevive()
    {
        SetVisibility(true);
    }
}
