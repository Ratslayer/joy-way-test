using UnityEngine;
using UnityEngine.InputSystem;

public class ExitOnPress : MonoBehaviour
{
    [SerializeField]
    private Key _key;
    private void Update()
    {
        if (Keyboard.current[_key].wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;                    
#else
            Application.Quit();
#endif
        }
    }
}
