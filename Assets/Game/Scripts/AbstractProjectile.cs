using System.Collections;
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
        StartCoroutine(DespawnCoroutine());
    }
    private IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Despawn();
    }
    protected void Despawn()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out HitTarget hitTarget))
            OnHit(hitTarget.Root, collision);
        Despawn();
    }
    protected abstract void OnHit(Transform root, Collision collision);
}
