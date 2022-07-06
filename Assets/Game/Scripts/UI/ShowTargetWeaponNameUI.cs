using TMPro;
using UnityEngine;
//shows which weapon we are looking at right now
public class ShowTargetWeaponNameUI : MonoBehaviour
{
    [SerializeField]
    private WeaponInteraction _interaction;
    [SerializeField]
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text.text = "";
    }
    private void OnEnable()
    {
        _interaction.WeaponChanged += OnWeaponChange;
    }
    private void OnDisable()
    {
        _interaction.WeaponChanged -= OnWeaponChange;
    }
    private void OnWeaponChange(AbstractWeapon weapon)
    {
        _text.text = weapon ? weapon.name : "";
    }
}