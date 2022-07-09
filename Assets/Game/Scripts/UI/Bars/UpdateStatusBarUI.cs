using UnityEngine;

public class UpdateStatusBarUI : MonoBehaviour
{
    [SerializeField]
    private CharacterStatusContainer _statuses;
    [SerializeField]
    private CharacterStatus _status;
    [SerializeField]
    private BarUI _barUI;
    private bool _show;
    private float _remainingTime, _duration;
    private void OnStatusLost(CharacterStatus status)
    {
        if (status == _status)
        {
            _show = false;
            _barUI.Hide();
        }
    }
    private void OnStatusGained(in CharacterStatusContainer.StatusGainedContext context)
    {
        if (context._status == _status)
        {
            _show = true;
            _remainingTime = _duration = context._duration;
            _barUI.UpdateValues(_duration, _duration);
        }
    }
    private void OnEnable()
    {
        _statuses.StatusLost += OnStatusLost;
        _statuses.StatusGained += OnStatusGained;
    }
    private void OnDisable()
    {
        _statuses.StatusLost -= OnStatusLost;
        _statuses.StatusGained -= OnStatusGained;
    }
    private void Awake()
    {
        _barUI.Hide();
    }
    private void Update()
    {
        if (_show)
        {
            _remainingTime -= Time.deltaTime;
            _barUI.UpdateValues(_remainingTime, _duration);
        }
    }
}
