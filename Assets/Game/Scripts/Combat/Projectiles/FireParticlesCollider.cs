using System;
using System.Collections.Generic;
using UnityEngine;

public class FireParticlesCollider : AbstractParticleCollider
{
    [Serializable]
    private class ResourceData
    {
        public CharacterResource _resource;
        public float _value;
    }
    [SerializeField]
    private List<ResourceData> _resourceValues = new List<ResourceData>();
    [SerializeField]
    private CharacterStatus _burningStatus, _wetStatus;
    [SerializeField]
    private float _burnDuration = 10f;
    protected override void OnParticlesHit(Transform root, int numCollisions)
    {
        if (root.TryGetComponent(out CharacterResourceContainer resources))
            foreach (var data in _resourceValues)
                resources.AddValue(data._resource, data._value * numCollisions);
        if (root.TryGetComponent(out CharacterStatusContainer container)
            && !container.HasStatus(_wetStatus))
            container.AddStatus(_burningStatus, _burnDuration);
    }

}