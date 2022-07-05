using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private AbstractWeapon _weaponPrefab;
    [SerializeField]
    private Transform _weaponAnchor;
    private AbstractWeapon _weaponInstance;
    private void Awake()
    {
        _weaponInstance = Instantiate(_weaponPrefab);
        _weaponInstance.Unequipped += OnUnequip;
        OnUnequip();
    }
    private void OnUnequip()
    {
        _weaponInstance.transform.parent = _weaponAnchor;
        _weaponInstance.transform.localPosition = Vector3.zero;
        _weaponInstance.transform.localRotation = Quaternion.identity;
    }
}