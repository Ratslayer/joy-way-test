using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private void Update()
    {
        var dir = _camera.transform.position - transform.position;
        dir.y = 0f;
        transform.forward = dir;
    }
}
