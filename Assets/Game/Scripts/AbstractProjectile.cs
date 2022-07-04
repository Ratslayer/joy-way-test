using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class AbstractProjectile : MonoBehaviour
{

    [SerializeField]
    private float _impulse = 10f, _lifeTime = 3f;
    private Rigidbody _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public void Shoot(Transform muzzle)
    {
        transform.position = muzzle.position;
        _rigidBody.AddForce(muzzle.forward * _impulse, ForceMode.Impulse);
        Destroy(gameObject, _lifeTime);
    }
}