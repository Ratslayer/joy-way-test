using TMPro;
using UnityEngine;
using UnityEngine.UI;
//base class for all ui bars in game
//controls value text (if available) and image fill amount
public class BarUI : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private CanvasGroup _hideGroupOnZero;
    [SerializeField]
    private bool _hideOnZero;
    [SerializeField]
    private Gradient _colorGradient;
    public void UpdateValues(float value, float maxValue)
    {
        if (_hideGroupOnZero && _hideOnZero)
            _hideGroupOnZero.alpha = value < Mathf.Epsilon ? 0f : 1f;
        if (_text)
            _text.text = value.ToString("N0");
        if (_barImage)
        {
            var factor = value / maxValue;
            _barImage.fillAmount = factor;
            _barImage.color = _colorGradient.Evaluate(factor);
        }
    }
}
