using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentInventory : MonoBehaviour
{
    [Serializable]
    private class HandData
    {
        public Key _key;
        public Transform _handTransform;
        public Weapon _weapon;
        private EquipmentInventory _inventory;
        public void Init(EquipmentInventory inventory)
        {
            _inventory = inventory;
        }
        public void UpdateWeapon()
        {
            if (!Keyboard.current[_key].wasPressedThisFrame)
                return;
            //update weapon
            if (_weapon)
            {
                _weapon.transform.SetParent(null, true);
                _weapon.Unequip();
                _weapon = null;
            }
            else if (_inventory._targetWeapon)
            {
                _weapon = _inventory._targetWeapon;
                _weapon.transform.SetParent(_handTransform, true);
                _weapon.transform.localPosition = Vector3.zero;
                _weapon.transform.localRotation = Quaternion.identity;
            }
        }
    }
    [SerializeField]
    private HandData _rightHand, _leftHand;
    [SerializeField]
    private Camera _camera;
    private Weapon _targetWeapon;
    private void Start()
    {
        _rightHand.Init(this);
        _leftHand.Init(this);
    }
    private void Update()
    {
        //update target weapon
        _targetWeapon = null;
        var hits = Physics.RaycastAll(_camera.transform.position, _camera.transform.forward);
        foreach (var hit in hits)
            if (hit.collider.TryGetComponent<Weapon>(out var weapon))
            {
                _targetWeapon = weapon;
                break;
            }
        //update hands
        _rightHand.UpdateWeapon();
        _leftHand.UpdateWeapon();
    }

}
