using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
[RequireComponent(typeof(WeaponInteraction))]
public class WeaponInventory : MonoBehaviour
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
        private WeaponInteraction _interaction;
        public void Init(WeaponInteraction interaction)
        {
            _interaction = interaction;
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
            else if (_interaction.CurrentWeapon)
            {
                _weapon = _interaction.CurrentWeapon;
                _weapon.transform.SetParent(_handTransform, true);
                _weapon.transform.localPosition = Vector3.zero;
                _weapon.transform.localRotation = Quaternion.identity;
            }
        }
    }
    [SerializeField]
    private HandData _rightHand, _leftHand;
    private WeaponInteraction _interaction;
    private void Awake()
    {
        _interaction = GetComponent<WeaponInteraction>();
        _rightHand.Init(_interaction);
        _leftHand.Init(_interaction);
    }
    private void Update()
    {
        _rightHand.UpdateWeapon();
        _leftHand.UpdateWeapon();
    }

}
