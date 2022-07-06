using UnityEngine;
public class HideObjectsOnDeath : HealthComponent
{
    [SerializeField]
    private Transform[] _objectsToHide;
    private void OnEnable()
    {
        Health.HealedToFull += OnRevive;
        Health.ReachedZero += OnDeath;
    }
    private void OnDisable()
    {
        Health.HealedToFull -= OnRevive;
        Health.ReachedZero -= OnDeath;
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
