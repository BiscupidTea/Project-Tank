using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Primary Shoot")]
    [SerializeField] private Weapon primaryWeapon;

    [Header("Secondary Shoot")]
    [SerializeField] private Weapon secondaryWeapon;

    [Header("Sounds Shoot")]
    //TODO: Fix - Unclear name
    [SerializeField] private AudioClip shootanimation1;
    [SerializeField] private AudioClip shootanimation2;

    private float timeAudio;
    private float currentTimeAudio;
    private bool primaryShootSelected;

    private void Awake()
    {
        //TODO: TP2 - SOLID
        timeAudio = shootanimation2.length;
        currentTimeAudio = timeAudio;

        primaryShootSelected = true;
    }

    public void SwitchWeapon()
    {
        primaryShootSelected = !primaryShootSelected;
    }

    public void ShootWeapon()
    {
        //TODO: TP2 - Strategy
        if (primaryShootSelected) 
        { 
            ShootPrimaryWeapon();
        }
        else
        {
            ShootSecondaryLogic();
        }
    }

    private void ShootPrimaryWeapon()
    {
        primaryWeapon.Shoot();
        //TODO: TP2 - SOLID
        SoundManager.Instance.PlaySound(shootanimation1);
    }

    private void ShootSecondaryLogic()
    {
        secondaryWeapon.Shoot();
        if (currentTimeAudio >= timeAudio)
        {
            //TODO: TP2 - SOLID
            SoundManager.Instance.PlaySound(shootanimation2, 0.4f);
            currentTimeAudio = 0;
        }
    }

    //TODO: Fix - Unclear name
    //TODO: Fix - Should be native Setter/Getter
    public bool WeaponUsing()
    {
        return primaryShootSelected;
    }
}