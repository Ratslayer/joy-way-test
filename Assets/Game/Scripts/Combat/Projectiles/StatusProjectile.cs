using UnityEngine;

public class StatusProjectile : AbstractProjectile
{
    [SerializeField]
    private int _wetness = 0;
    protected override void OnHit(Transform root, Collision collision)
    {
        if (root.TryGetComponent(out WetHotStatus status))
            status.AddWetness(_wetness);
    }
}