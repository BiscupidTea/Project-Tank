using UnityEngine;

/// <summary>
/// Controls the player shoot
/// </summary>
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Weapon[] specialWeapons;

    private Weapon weaponInUse;

    private int normalWeaponInUse;

    private int totalWeapons;
    private int totalSpecialWeapons;

    public int TotalWeapons { get => totalWeapons; set => totalWeapons = value; }
    public Weapon WeaponInUse { get => weaponInUse; set => weaponInUse = value; }

    private void Awake()
    {
        TotalWeapons = weapons.Length;
        totalSpecialWeapons = specialWeapons.Length;
        normalWeaponInUse = 0;
        WeaponInUse = weapons[normalWeaponInUse];
    }

    /// <summary>
    /// shoot the weapon selected
    /// </summary>
    public void ShootWeapon()
    {
        WeaponInUse.Shoot();
    }

    /// <summary>
    /// switch to the next weapon in the arsenal, if pass this limit returns to the first weapon
    /// </summary>
    public void SwitchToNextWeapon()
    {
        normalWeaponInUse++;
        if (normalWeaponInUse > totalWeapons - 1)
        {
            normalWeaponInUse = 0;
        }
        WeaponInUse = weapons[normalWeaponInUse];
    }

    public void SwitchToArtillery()
    {
        for (int i = 0; i < totalSpecialWeapons; i++)
        {
            if (specialWeapons[i].GetComponent<ArtilleryWeapon>())
            {
                if (WeaponInUse == specialWeapons[i])
                {
                    WeaponInUse = weapons[normalWeaponInUse];
                    break;
                }

                WeaponInUse = specialWeapons[i];
                break;
            }
        }
    }
}