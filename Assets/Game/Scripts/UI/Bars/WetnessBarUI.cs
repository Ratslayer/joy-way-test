using UnityEngine;
using static WetHotStatus;
public class WetnessBarUI : BarUI
{
    [SerializeField]
    private WetHotStatus _status;
    private void OnWetnessChange()
    {
        UpdateValues(_status.CurrentWetness, _status.MaxWetness);
    }
    private void OnStatusChange(Status status) => OnWetnessChange();
    private void OnEnable()
    {
        _status.WetnessChanged += OnWetnessChange;
        _status.StatusChanged += OnStatusChange;
        OnWetnessChange();
    }
    private void OnDisable()
    {
        _status.WetnessChanged -= OnWetnessChange;
        _status.StatusChanged -= OnStatusChange;
    }

}
