using UnityEngine;
using static WetHotStatus;
public class Bullet : AbstractProjectile
{
    [SerializeField]
    private float _damage = 20, _damageAmplification = 10f;
    protected override void OnHit(Transform root, Collision collision)
    {
        if (root.TryGetComponent(out Health health))
        {
            var damage = _damage;
            //apply status damage amplification
            if (root.TryGetComponent(out WetHotStatus status))
                damage += status.CurrentStatus switch
                {
                    Status.Burning => _damageAmplification,
                    Status.Wet => -_damageAmplification,
                    _ => 0
                };
            health.TakeDamage(damage, collision);
        }
    }
}
