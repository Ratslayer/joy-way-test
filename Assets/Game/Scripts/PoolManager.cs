using System.Collections.Generic;
using UnityEngine;

public class PoolManager : AbstractManager<PoolManager>
{
    private readonly Dictionary<GameObject, List<GameObject>> _instances = new Dictionary<GameObject, List<GameObject>>();
    public GameObject Spawn(GameObject prefab, Transform parent)
    {
        if (prefab == null)
            return null;
        if (!_instances.TryGetValue(prefab, out var instances))
        {
            instances = new List<GameObject>();
            _instances.Add(prefab, instances);
        }
        GameObject instance = null;
        foreach (var item in instances)
            if (!item.activeSelf)
            {
                instance = item;
                break;
            }
        if (!instance)
        {
            instance = Instantiate(prefab);
            instances.Add(instance);
        }
        if (!parent)
            parent = transform;
        instance.transform.SetParent(parent, true);
        instance.SetActive(true);
        return instance;
    }
    public TComponent Spawn<TComponent>(TComponent prefab, Transform parent = null) where TComponent : Component
    {
        var instance = Spawn(prefab.gameObject, parent);
        return instance.GetComponent<TComponent>();
    }
}