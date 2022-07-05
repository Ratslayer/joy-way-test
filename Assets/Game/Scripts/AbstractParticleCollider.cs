using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class AbstractParticleCollider : MonoBehaviour
{
    private ParticleSystem _system;
    private List<ParticleCollisionEvent> _collisions = new List<ParticleCollisionEvent>();
    private void Awake()
    {
        _system = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out HitTarget hitTarget))
        {
            var numCollisions = _system.GetCollisionEvents(other, _collisions);
            OnParticlesHit(hitTarget.Root, numCollisions);
        }
    }
    protected abstract void OnParticlesHit(Transform root, int numCollisions);
}
