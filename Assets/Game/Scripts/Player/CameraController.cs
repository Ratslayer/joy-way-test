using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private float _lookSpeed = 100f;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
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
}
