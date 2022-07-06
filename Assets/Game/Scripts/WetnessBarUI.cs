using UnityEngine;
public class WetnessBarUI : AbstractBarUI
{
    [SerializeField]
    private WetHotStatus _status;
    private void OnWetnessChange()
    {
        UpdateValues(_status.CurrentWetness, _status.MaxWetness);
    }
    private void OnEnable()
    {
        _status.WetnessChanged += OnWetnessChange;
        OnWetnessChange();
    }
    private void OnDisable()
    {
        _status.WetnessChanged -= OnWetnessChange;
    }

}
