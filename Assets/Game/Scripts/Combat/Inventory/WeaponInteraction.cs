using System;
using UnityEngine;
//this class keeps track of which weapon we are looking at right now
public class WeaponInteraction : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private AbstractWeapon _targetWeapon;
    public event Action<AbstractWeapon> WeaponChanged;
    public AbstractWeapon CurrentWeapon => _targetWeapon;
    private void Update()
    {
        var previousWeapon = _targetWeapon;
        _targetWeapon = null;
        //find first weapon we are looking at
        var hits = Physics.RaycastAll(_camera.transform.position, _camera.transform.forward);
        foreach (var hit in hits)
            if (hit.collider.TryGetComponent<AbstractWeapon>(out var weapon))
            {
                _targetWeapon = weapon;
                break;
            }
        if (_targetWeapon != previousWeapon)
            WeaponChanged?.Invoke(_targetWeapon);
    }
}
