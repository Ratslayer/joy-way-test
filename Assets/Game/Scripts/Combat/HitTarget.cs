using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _root;
    public Transform Root => _root;
}