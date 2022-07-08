using System;
using UnityEngine;
//keeps track of current status
//controls the rules for status change (max wetness and burn duration)
//clears all statuses on revive
//[RequireComponent(typeof(Health))]
public class WetHotStatus : MonoBehaviour
{
    [SerializeField]
    private int _maxWetness = 100;
    [SerializeField]
    private float _maxBurnDuration = 10f;
    private float _remainingBurnDuration = 0f;
    private int _currentWetness = 0;
    public event Action<float> BurnedForDuration;
    public event Action WetnessChanged;
    public int MaxWetness => _maxWetness;
    public int CurrentWetness => _currentWetness;
    public float MaxBurnDuration => _maxBurnDuration;
    public float RemainingBurnDuration => _remainingBurnDuration;
    public bool IsBurning => _remainingBurnDuration > 0f;
    public bool IsWet => _currentWetness > 0;
    #region Status
    public enum Status
    {
        None,
        Wet,
        Burning
    }
    public event Action<Status> StatusChanged;
    private Status _lastStatus;
    public Status CurrentStatus
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
    //private Health _health;
    //private void Awake()
    //{
    //    _health = GetComponent<Health>();
    //}
    //private void OnEnable()
    //{
    //    _health.HealedToFull += OnHealToFull;
    //}
    //private void OnDisable()
    //{
    //    _health.HealedToFull -= OnHealToFull;
    //}
    //private void OnHealToFull()
    //{
    //    _currentWetness = 0;
    //    _remainingBurnDuration = 0f;
    //    StatusChanged?.Invoke(Status.None);
    //}
    private void Update()
    {
        if (IsBurning)
        {
            BeginStatusChangeCheck();
            var time = Mathf.Min(Time.deltaTime, _remainingBurnDuration);
            _remainingBurnDuration -= time;
            if (_remainingBurnDuration < Mathf.Epsilon)
                StopBurn();
            BurnedForDuration?.Invoke(time);
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
                SetWetness(value);
                StopBurn();
            }
            else UpdateBurnDuration();
        }
        else
        {
            SetWetness(Mathf.Clamp(_currentWetness + value, 0, _maxWetness));
            if (value < 0 && !IsWet)
                UpdateBurnDuration();
        }
        EndStatusChangeCheck();
    }
    private void SetWetness(int value)
    {
        if (_currentWetness != value)
        {
            _currentWetness = value;
            WetnessChanged?.Invoke();
        }
    }
    private void StopBurn() => _remainingBurnDuration = 0f;
    private void UpdateBurnDuration() => _remainingBurnDuration = _maxBurnDuration;

}