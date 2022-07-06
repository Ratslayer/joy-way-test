using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
//move is controlled by a composite vector input (WASD)
//so I need to define it in an actions asset
//everywhere else I subscribe to input (mouse/keyboard) directly for simplicity's sake
[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actions;
    [SerializeField]
    private string _moveActionName;
    [SerializeField]
    private float _moveSpeed = 3;
    private CharacterController _characterController;
    private Vector3 _moveInputDir;
    private InputAction _moveAction;
    //called on input change only
    private void Move(CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();
        _moveInputDir = transform.forward * moveInput.y + transform.right * moveInput.x;
    }
    private void Update()
    {
        _characterController.Move(_moveInputDir * _moveSpeed * Time.deltaTime);
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _moveAction= _actions[_moveActionName];
        _moveAction.Enable();
        
    }
    private void OnEnable()
    {
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
    }
    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
    }
    
    
}
