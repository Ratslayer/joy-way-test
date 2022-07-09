using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusContainer : MonoBehaviour
{
    [SerializeField]
    private float _tickDuration = 1f;
    private class StatusData
    {
        public bool Timed { get; private set; }
        public float Duration { get; private set; }
        public float RemainingTime { get; private set; }
        public float AccumulatedTime { get; private set; }
        public StatusData(float duration)
        {
            SetDuration(duration);
            AccumulatedTime = 0f;
        }
        public void SetDuration(float duration)
        {
            Timed = duration > Mathf.Epsilon;
            Duration = duration;
            RemainingTime = duration;
        }
        public bool UpdateTime(float delta, float tickDuration)
        {
            RemainingTime -= delta;
            AccumulatedTime += delta;
            if (AccumulatedTime > tickDuration)
            {
                AccumulatedTime -= tickDuration;
                return true;
            }
            return false;
        }
    }
    public readonly struct StatusGainedContext
    {
        public readonly CharacterStatus _status;
        public readonly bool _timed;
        public readonly float _duration;
        public StatusGainedContext(CharacterStatus status, float duration)
        {
            _status = status;
            _timed = duration > Mathf.Epsilon;
            _duration = duration;
        }
    }
    public readonly struct StatusTickContext
    {
        public readonly CharacterStatus _status;
        public readonly float _tickDuration, _totalDuration, _remainingTime;
        public StatusTickContext(CharacterStatus status, float tickDuration, float totalDuration, float remainingTime)
        {
            _status = status;
            _tickDuration = tickDuration;
            _totalDuration = totalDuration;
            _remainingTime = remainingTime;
        }
    }
    private readonly Dictionary<CharacterStatus, StatusData> _statuses = new Dictionary<CharacterStatus, StatusData>();
    private readonly List<CharacterStatus> _deadBuffs = new List<CharacterStatus>();
    public event Action<CharacterStatus> StatusLost;
    public delegate void StatusGainedCallback(in StatusGainedContext context);
    public event StatusGainedCallback StatusGained;
    public delegate void StatusTickedCallback(in StatusTickContext context);
    public event StatusTickedCallback StatusTicked;
    public bool HasStatus(CharacterStatus status) => _statuses.ContainsKey(status);
    public void AddStatus(CharacterStatus status, float duration = 0f)
    {
        if (!_statuses.TryGetValue(status, out var data))
        {
            _statuses.Add(status, new StatusData(duration));
            TriggerEvent();
        }
        else if (duration > 0f)
        {
            data.SetDuration(duration);
            TriggerEvent();
        }
        void TriggerEvent() => StatusGained?.Invoke(new StatusGainedContext(status, duration));
    }
    public void RemoveStatus(CharacterStatus status)
    {
        if (HasStatus(status))
        {
            RemoveBuffInternal(status);
        }
    }
    private void Update()
    {
        _deadBuffs.Clear();
        var time = Time.deltaTime;
        foreach (var kvp in _statuses)
        {
            var data = kvp.Value;
            if (!data.Timed)
                continue;
            if (data.UpdateTime(time, _tickDuration))
                StatusTicked?.Invoke(new StatusTickContext(kvp.Key, _tickDuration, data.Duration, data.RemainingTime));
            if (data.RemainingTime < Mathf.Epsilon)
                _deadBuffs.Add(kvp.Key);
        }
        foreach (var status in _deadBuffs)
            RemoveBuffInternal(status);
    }
    private void RemoveBuffInternal(CharacterStatus status)
    {
        _statuses.Remove(status);
        StatusLost?.Invoke(status);
    }
}