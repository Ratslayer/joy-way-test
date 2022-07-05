using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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
        public AbstractWeapon _weapon;
        private EquipmentInventory _inventory;
        public void Init(EquipmentInventory inventory)
        {
            _inventory = inventory;
        }
        private ButtonControl AttackButton
            => _mouseButton == MouseButton.Left
                ? Mouse.current.leftButton : Mouse.current.rightButton;
        public void UpdateWeapon()
        {
            if (Keyboard.current[_equipKey].wasPressedThisFrame)
                EquipWeapon();
            if (_weapon)
            {
                var button = AttackButton;
                if (button.wasPressedThisFrame)
                    _weapon.BeginAttack();
                else if (button.wasReleasedThisFrame)
                    _weapon.EndAttack();
            }
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
    private AbstractWeapon _targetWeapon;
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
            if (hit.collider.TryGetComponent<AbstractWeapon>(out var weapon))
            {
                _targetWeapon = weapon;
                break;
            }
        //update hands
        _rightHand.UpdateWeapon();
        _leftHand.UpdateWeapon();
    }

}
