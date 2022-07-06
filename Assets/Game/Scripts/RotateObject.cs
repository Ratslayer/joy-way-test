using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotationSpeed;
    private void Update()
    {
        transform.rotation *= Quaternion.Euler(_rotationSpeed * Time.deltaTime);
    }
}
