using UnityEngine;
//colliders are often hidden in a hierarchy
//this class allows others to find hierarchy root
//which contains all the components
public class HitTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _root;
    public Transform Root => _root;
}