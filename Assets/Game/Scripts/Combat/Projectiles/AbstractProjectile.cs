using System.Collections;
using UnityEngine;
//base class for all projectiles
//controls spawn velocity and default collision behaviour
[RequireComponent(typeof(Rigidbody))]
public abstract class AbstractProjectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f, _lifeTime = 3f;
    private Rigidbody _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public void Shoot(Transform muzzle)
    {
        transform.position = muzzle.position;
        _rigidBody.velocity = muzzle.forward * _speed;
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
        gameObject.SetActive(false);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out HitTarget hitTarget))
            OnHit(hitTarget.Root, collision);
        Despawn();
    }
    protected abstract void OnHit(Transform root, Collision collision);
}
