using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class EquipmentInventory : MonoBehaviour
{
    private enum MouseButton
    {
        Left,
        Right
    }
    [Serializable]
    private class HandData
    {
        public Key _equipKey;
        public MouseButton _mouseButton;
        public Transform _handTransform;
        public Weapon _weapon;
        private EquipmentInventory _inventory;
        public void Init(EquipmentInventory inventory)
        {
            _inventory = inventory;
        }
        public void UpdateWeapon()
        {
            if (Keyboard.current[_equipKey].wasPressedThisFrame)
                EquipWeapon();
            if (Mouse.current.rightButton.wasPressedThisFrame
                && _mouseButton == MouseButton.Right
                || Mouse.current.leftButton.wasPressedThisFrame
                && _mouseButton == MouseButton.Left)
                Attack();
        }
        private void Attack()
        {
            if (_weapon)
                _weapon.Attack();
        }
        private void EquipWeapon()
        {
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
