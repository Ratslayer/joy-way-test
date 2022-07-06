using UnityEngine;
using static WetHotStatus;
//changes renderer material depending on its status
[RequireComponent(typeof(WetHotStatus))]
public class ChangeStatusMaterial : MonoBehaviour
{
    [SerializeField]
    private Material _defaultMaterial, _wetMaterial, _burningMaterial;
    [SerializeField]
    private Renderer _renderer;
    private WetHotStatus _status;
    private void SetMaterial(Material material) => _renderer.sharedMaterial = material;
    private void OnStatusChange(Status status)
    {
        var material = status switch
        {
            Status.Burning => _burningMaterial,
            Status.Wet => _wetMaterial,
            _ => _defaultMaterial
        };
        SetMaterial(material);
    }
    private void Awake()
    {
        _status = GetComponent<WetHotStatus>();
        SetMaterial(_defaultMaterial);
    }
    private void OnEnable()
    {
        _status.StatusChanged += OnStatusChange;
    }
    private void OnDisable()
    {
        _status.StatusChanged -= OnStatusChange;
    }
}