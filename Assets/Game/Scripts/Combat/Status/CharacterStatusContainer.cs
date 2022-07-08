using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusContainer : MonoBehaviour
{
    private readonly List<CharacterStatus> _statuses = new List<CharacterStatus>();
    public event Action<CharacterStatus> StatusGained, StatusLost;
    public bool HasStatus(CharacterStatus status) => _statuses.Contains(status);
    public void AddStatus(CharacterStatus status)
    {
        if (!HasStatus(status))
        {
            _statuses.Add(status);
            StatusGained?.Invoke(status);
        }
    }
    public void RemoveStatus(CharacterStatus status)
    {
        if (HasStatus(status))
        {
            _statuses.Remove(status);
            StatusLost?.Invoke(status);
        }
    }
}