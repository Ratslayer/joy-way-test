using System;
using System.Collections.Generic;
using UnityEngine;
public class CharacterResourceContainer : MonoBehaviour
{
    [Serializable]
    private class ResourceData
    {
        public CharacterResource _resource;
        public float _value, _maxValue;
        public float GetClamped(float value) => Mathf.Clamp(value, 0f, _maxValue);
    }
    [SerializeField]
    private List<ResourceData> _resourceDatas = new List<ResourceData>();
    public readonly struct ResourceChangeContext
    {
        public readonly CharacterResource _resource;
        public readonly float _oldValue, _newValue, _maxValue, _delta;
        public ResourceChangeContext(CharacterResource resource, float oldValue, float newValue, float delta, float maxValue)
        {
            _resource = resource;
            _oldValue = oldValue;
            _newValue = newValue;
            _delta = delta;
            _maxValue = maxValue;
        }
    }
    public delegate void ResourceChangeCallback(in ResourceChangeContext context);
    public event ResourceChangeCallback ResourceChanged;
    public void AddValue(CharacterResource resource, float value)
    {
        if (HasResource(resource, out var data))
            SetValue(data, data._value + value);
    }
    public void SetValue(CharacterResource resource, float newValue)
    {
        if (HasResource(resource, out var data))
            SetValue(data, newValue);
    }
    public void SetToMaxValue(CharacterResource resource)
    {
        if (HasResource(resource, out var data))
            SetValue(data, data._maxValue);
    }
    private bool HasResource(CharacterResource resource, out ResourceData data)
        => _resourceDatas.TryGet(d => d._resource == resource, out data);
    private void SetValue(ResourceData data, float value)
    {
        var oldValue = data._value;
        var newValue = data.GetClamped(value);
        var delta = newValue - oldValue;
        if (Mathf.Abs(delta) > Mathf.Epsilon)
        {
            data._value = newValue;
            ResourceChanged?.Invoke(new ResourceChangeContext(data._resource, oldValue, newValue, delta, data._maxValue));
        }
    }
    private void Start()
    {
        foreach(var data in _resourceDatas)
            ResourceChanged?.Invoke(new ResourceChangeContext(data._resource, data._value, data._value, 0, data._maxValue));
    }
}
