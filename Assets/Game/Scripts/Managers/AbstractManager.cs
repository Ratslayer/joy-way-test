using System.Collections.Generic;
using UnityEngine;
//base singleton class for all managers
public abstract class AbstractManager<TSelf> : MonoBehaviour where TSelf : AbstractManager<TSelf>
{
    public static TSelf Instance { get; private set; }
    protected virtual void Awake()
    {
        Instance = this as TSelf;
    }
}
