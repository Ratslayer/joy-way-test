using System.Collections.Generic;
using UnityEngine;
//manages spawned objects based on their original prefab
public class PoolManager : AbstractManager<PoolManager>
{
    //key: prefab
    //value: all existing instances, both active and inactive
    private readonly Dictionary<GameObject, List<GameObject>> _instances = new Dictionary<GameObject, List<GameObject>>();
    //fetches first inactive instance of this prefab
    //if none exist, creates a new one
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
        if (!parent)
            parent = transform;
        if (!instance)
        {
            instance = Instantiate(prefab, parent);
            instances.Add(instance);
        }
        else instance.transform.SetParent(parent, true);
        instance.SetActive(true);
        return instance;
    }
    public TComponent Spawn<TComponent>(TComponent prefab, Transform parent = null) where TComponent : Component
    {
        var instance = Spawn(prefab.gameObject, parent);
        return instance.GetComponent<TComponent>();
    }
}