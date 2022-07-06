using UnityEngine;

public class FireParticlesCollider : AbstractParticleCollider
{
    [SerializeField]
    private int _hotnessPerParticle = 1;
    protected override void OnParticlesHit(Transform root, int numCollisions)
    {
        if (root.TryGetComponent(out WetHotStatus status))
            status.AddWetness(-_hotnessPerParticle * numCollisions);
    }

}