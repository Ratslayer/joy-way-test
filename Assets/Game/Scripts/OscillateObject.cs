using UnityEngine;
public class OscillateObject : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private Vector3 _from, _to;
    private float _elapsedTime;
    private void OnEnable()
    {
        transform.localPosition = _from;
    }
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        var factor = Mathf.Cos(_elapsedTime);
        transform.localPosition = Vector3.LerpUnclamped(_from, _to, factor);
    }
}