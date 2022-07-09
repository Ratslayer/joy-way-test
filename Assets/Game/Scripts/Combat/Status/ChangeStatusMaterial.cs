using System;
using System.Collections.Generic;
using UnityEngine;
//changes renderer material depending on its status
public class ChangeStatusMaterial : MonoBehaviour
{
    [Serializable]
    private class StatusMaterial
    {
        public CharacterStatus _status;
        public Material _material;
    }
    [SerializeField]
    private CharacterStatusContainer _characterStatusContainer;
    [SerializeField]
    private Material _defaultMaterial;
    [SerializeField]
    private List<StatusMaterial> _materialDatas = new List<StatusMaterial>();
    [SerializeField]
    private Renderer _renderer;
    private void SetMaterial(Material material) => _renderer.sharedMaterial = material;
    private void OnStatusChange(in CharacterStatusContainer.StatusGainedContext context) => OnStatusChange(context._status);
    private void OnStatusChange(CharacterStatus status)
    {
        foreach (var data in _materialDatas)
            if (_characterStatusContainer.HasStatus(data._status))
            {
                SetMaterial(data._material);
                return;
            }
        SetMaterial(_defaultMaterial);
    }
    private void Awake()
    {
        SetMaterial(_defaultMaterial);
    }
    private void OnEnable()
    {
        _characterStatusContainer.StatusGained += OnStatusChange;
        _characterStatusContainer.StatusLost += OnStatusChange;
    }
    private void OnDisable()
    {
        _characterStatusContainer.StatusGained -= OnStatusChange;
        _characterStatusContainer.StatusLost -= OnStatusChange;
    }
}
