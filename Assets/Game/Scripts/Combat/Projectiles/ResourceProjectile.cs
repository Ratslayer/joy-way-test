using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProjectile : AbstractProjectile
{
    [SerializeField]
    private CharacterResource _resource;
    [SerializeField]
    private float _value;
    [Serializable]
    private class StatusModifier
    {
        public CharacterStatus _status;
        public float _value;
    }
    [SerializeField]
    private List<StatusModifier> _statusModifiers = new List<StatusModifier>();
    protected override void OnHit(Transform root, Collision collision)
    {
        if (root.TryGetComponent(out CharacterResourceContainer resourceContainer))
        {
            var value = _value;
            if (root.TryGetComponent(out CharacterStatusContainer statusContainer))
                foreach (var modifier in _statusModifiers)
                    if (statusContainer.HasStatus(modifier._status))
                        value += modifier._value;
            resourceContainer.AddValue(_resource, value);
        }
    }
}