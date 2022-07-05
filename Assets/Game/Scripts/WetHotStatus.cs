using System;
using UnityEngine;
public class WetHotStatus : MonoBehaviour
{
    [SerializeField]
    private int _maxWetness = 100;
    [SerializeField]
    private float _maxBurnDuration = 10f;

    private float _remainingBurnDuration = 0f;
    private int _currentWetness = 0;
    public event Action<float> BurnedForDuration;
    private bool IsBurning => _remainingBurnDuration > 0f;
    private bool IsWet => _currentWetness > 0;
    #region Status
    public enum Status
    {
        None,
        Wet,
        Burning
    }
    public event Action<Status> StatusChanged;
    private Status _lastStatus;
    private Status CurrentStatus
       => _remainingBurnDuration > 0f
           ? Status.Burning
           : _currentWetness > 0
               ? Status.Wet
               : Status.None;
    private void BeginStatusChangeCheck() => _lastStatus = CurrentStatus;
    private void EndStatusChangeCheck()
    {
        var current = CurrentStatus;
        if (_lastStatus != current)
            StatusChanged?.Invoke(current);
    }
    #endregion

    private void Update()
    {
        if (IsBurning)
        {
            BeginStatusChangeCheck();
            var time = Mathf.Min(Time.deltaTime, _remainingBurnDuration);
            BurnedForDuration?.Invoke(time);
            _remainingBurnDuration -= time;
            if (_remainingBurnDuration < Mathf.Epsilon)
                StopBurn();
            EndStatusChangeCheck();
        }
    }

    public void AddWetness(int value)
    {
        if (value == 0)
            return;
        BeginStatusChangeCheck();
        if (IsBurning)
        {
            if (value > 0)
            {
                _currentWetness = value;
                StopBurn();
            }
            else UpdateBurnDuration();
        }
        else
        {
            _currentWetness = Mathf.Clamp(_currentWetness + value, 0, _maxWetness);
            if (value < 0 && !IsWet)
                UpdateBurnDuration();
        }
        EndStatusChangeCheck();
    }
    private void StopBurn() => _remainingBurnDuration = 0f;
    private void UpdateBurnDuration() => _remainingBurnDuration = _maxBurnDuration;

}