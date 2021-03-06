using UnityEngine;
//spawns weapon on awake from given prefab
//snaps weapon to its creation place on unequip
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
        _weaponInstance.name = _weaponPrefab.name;
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