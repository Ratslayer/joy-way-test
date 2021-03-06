using TMPro;
using UnityEngine;
//raises damage text up and fades it over time
public class DamageTextUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private float _height = 0.5f, _duration = 0.3f;
    private float _elapsedTime;
    private void OnEnable()
    {
        _elapsedTime = 0f;
    }
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _duration)
            gameObject.SetActive(false);
        else
        {
            var factor = _elapsedTime / _duration;
            transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up * _height, factor);
            _text.alpha = Mathf.Lerp(1, 0, factor);
        }
    }
}