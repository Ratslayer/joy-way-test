using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actions;
    [SerializeField]
    private string _moveAction;
    [SerializeField]
    private float _moveSpeed = 3, _lookSpeed = 100;
    [SerializeField]
    private Transform _cameraTransform;
    private CharacterController _characterController;
    private Vector3 _moveInputDir;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Subscribe(_moveAction, Move);
        _actions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Subscribe(string actionName, Action<CallbackContext> action) => Subscribe(actionName, action, action);
    private void Subscribe(string actionName, Action<CallbackContext> performed, Action<CallbackContext> canceled)
    {
        var action = _actions[actionName];
        action.performed += performed;
        action.canceled += canceled;
    }
    private void Update()
    {
        //update move
        _characterController.Move(_moveInputDir * _moveSpeed * Time.deltaTime);
        //update look
        var lookInput = Mouse.current.delta.ReadValue() * Time.smoothDeltaTime;
        var newPitch = _cameraTransform.eulerAngles.x - lookInput.y * _lookSpeed;
        //clamp between -180 and 180
        if (newPitch > 180)
            newPitch -= 360;
        var pitch = Mathf.Clamp(newPitch, -85, 85);

        _cameraTransform.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        transform.localEulerAngles += Vector3.up * lookInput.x * _lookSpeed;
    }
    private void Move(CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();
        _moveInputDir = transform.forward * moveInput.y + transform.right * moveInput.x;
    }
}
