using UnityEngine;

/// <summary>
/// Weapon Manager sound effect
/// </summary>
public class WeaponSFX : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private AudioClip weaponSound;

    private void Awake()
    {
        weapon.OnShoot += OnAction;
    }

    private void OnAction()
    {
        SoundManager.Instance.PlaySound(weaponSound);
    }
}
