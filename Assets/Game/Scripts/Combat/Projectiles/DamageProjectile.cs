using UnityEngine;
using static WetHotStatus;
//basic bullet that deals damage on collision
//deals more damage to burning targets and less to wet ones
public class DamageProjectile : AbstractProjectile
{
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private float _damage = 20, _damageAmplification = 10f;
    protected override void OnHit(Transform root, Collision collision)
    {
        if (root.TryGetComponent(out CharacterResourceContainer resourceContainer))
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
            resourceContainer.AddValue(_resource, -damage);
        }
    }
}
