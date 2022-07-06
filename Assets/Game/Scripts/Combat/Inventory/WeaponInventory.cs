using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
//keeps track of which weapons we have equipped
//equips/drops weapons on button press
//shoots weapons on mouse presses
[RequireComponent(typeof(WeaponInteraction))]
public class WeaponInventory : MonoBehaviour
{
    private enum MouseButton
    {
        Left,
        Right
    }
    //holds all data related to any given hand
    [Serializable]
    private class HandData
    {
        //key used to equip/drop weapon into this hand
        public Key _equipKey;
        //mouse button used to start/stop shooting this weapon
        public MouseButton _mouseButton;
        //where will the weapon model be positioned
        public Transform _handTransform;
        //currently equipped weapon
        public AbstractWeapon _weapon;
        private WeaponInteraction _interaction;
        public void Init(WeaponInteraction interaction)
        {
            _interaction = interaction;
        }
        private ButtonControl AttackButton
            => _mouseButton == MouseButton.Left
                ? Mouse.current.leftButton : Mouse.current.rightButton;
        public void OnUpdate()
        {
            if (Keyboard.current[_equipKey].wasPressedThisFrame)
                ToggleEquipWeapon();
            if (_weapon)
            {
                var button = AttackButton;
                if (button.wasPressedThisFrame)
                    _weapon.BeginAttack();
                else if (button.wasReleasedThisFrame)
                    _weapon.EndAttack();
            }
        }
        private void ToggleEquipWeapon()
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
        _rightHand.OnUpdate();
        _leftHand.OnUpdate();
    }
}
