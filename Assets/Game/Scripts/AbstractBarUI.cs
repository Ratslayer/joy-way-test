using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private CanvasGroup _hideGroupOnZero;
    protected void UpdateValues(float value, float maxValue)
    {
        if (_hideGroupOnZero)
            _hideGroupOnZero.alpha = value < Mathf.Epsilon ? 0f : 1f;
        if (_text)
            _text.text = value.ToString("N0");
        _barImage.fillAmount = value / maxValue;
    }
}
