using UnityEngine;

public class Bullet : AbstractProjectile
{
    [SerializeField]
    private float _damage = 20;
    protected override void OnHit(Transform root, Collision collision)
    {
        if (root.TryGetComponent(out Health health))
            health.TakeDamage(_damage);
    }
}
