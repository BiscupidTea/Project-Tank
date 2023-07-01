using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private Weapon[] weapons;

    private int weaponInUse;
    private int totalWeapons;

    public int WeaponInUse { get => weaponInUse; set => weaponInUse = value; }
    public int TotalWeapons { get => totalWeapons; set => totalWeapons = value; }

    private void Awake()
    {
        TotalWeapons = weapons.Length;
        weaponInUse = 0;
    }

    /// <summary>
    /// shoot the weapon selected
    /// </summary>
    public void ShootWeapon()
    {
        weapons[WeaponInUse].Shoot();
    }

    /// <summary>
    /// switch to the next weapon in the arsenal, if pass this limit returns to the first weapon
    /// </summary>
    public void SwitchToNextWeapon()
    {
        WeaponInUse++;
        if (WeaponInUse > totalWeapons-1) 
        {
            WeaponInUse = 0;
        }
    }
}