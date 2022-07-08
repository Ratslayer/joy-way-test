using UnityEngine;
using UnityEngine.InputSystem;

public class SetResourceToMaxOnPress : MonoBehaviour
{
    [SerializeField]
    private Key _fullHealKey;
    [SerializeField]
    private CharacterResourceContainer _resourceContainer;
    [SerializeField]
    private CharacterResource _resource;
    private void Update()
    {
        if (Keyboard.current[_fullHealKey].wasPressedThisFrame)
            _resourceContainer.SetToMaxValue(_resource);
    }
}
