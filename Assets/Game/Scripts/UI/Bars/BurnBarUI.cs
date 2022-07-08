using UnityEngine;

public class BurnBarUI : BarUI
{
    [SerializeField]
    private WetHotStatus _status;
    private void OnEnable()
    {
        _status.BurnedForDuration += OnBurnChange;
        _status.StatusChanged += OnStatusChange;
        ResetBurn();
    }
    private void OnDisable()
    {
        _status.BurnedForDuration -= OnBurnChange;
        _status.StatusChanged -= OnStatusChange;
    }
    private void OnBurnChange(float duration)
    {
        UpdateValues(_status.RemainingBurnDuration, _status.MaxBurnDuration);
    }
    private void ResetBurn()=> OnBurnChange(0);
    private void OnStatusChange(WetHotStatus.Status status)
    {
        if (status != WetHotStatus.Status.Burning)
            ResetBurn();
    }
}
