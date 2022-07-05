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
    public event Action BurnBegan, BurnEnded, WetBegan, WetEnded;
    public event Action<float> BurnedForDuration;
    private bool IsBurning => _remainingBurnDuration > 0f;
    private void Update()
    {
        if (IsBurning)
        {
            var time = Mathf.Min(Time.deltaTime, _remainingBurnDuration);
            BurnedForDuration?.Invoke(time);
            _remainingBurnDuration -= time;
            if (_remainingBurnDuration < Mathf.Epsilon)
                EndBurn();
        }
    }
    public void AddWetness(int value)
    {
        if (IsBurning)
        {
            EndBurn();
            _currentWetness = value;
            BeginWet();
        }
        else _currentWetness = Mathf.Min(_currentWetness + value, _maxWetness);
    }
    public void AddHotness(int value)
    {
        if (_currentWetness > 0)
        {
            _currentWetness = Mathf.Max(_currentWetness - value, 0);
            if (_currentWetness == 0)
            {
                EndWet();
                BeginBurn();
            }
        }
        else
        {
            if (!IsBurning)
                BeginBurn();
            _remainingBurnDuration = _maxBurnDuration;
        }
    }
    private void BeginWet() => WetBegan?.Invoke();
    private void EndWet() => WetEnded?.Invoke();
    private void BeginBurn() => BurnBegan?.Invoke();
    private void EndBurn()
    {
        _remainingBurnDuration = 0f;
        BurnEnded?.Invoke();
    }
}