using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootShellPosition;
    [SerializeField] Transform shootSecondaryPosition;


    [Header("Primary Shoot")]
    [SerializeField] private float reloadTimePrimary;
    [SerializeField] private float shootForcePrimary;

    [Header("Primary Shoot")]
    [SerializeField] private float reloadTimeSecondary;
    [SerializeField] private float rangeShootSecondary;
    [SerializeField] private float shootForceSecondary;

    private bool primaryShoot;
    private float currentReloadTimePrimary;
    private float currentReloadTimeSecondary;

    private bool readyToShootPrimary;
    private bool readyToShootSecondary;

    private void Awake()
    {
        readyToShootPrimary = true;
        readyToShootSecondary = false;
    }

    private void Update()
    {
        if (!readyToShootPrimary)
        {
            currentReloadTimePrimary += 1 * Time.deltaTime;
        }

        if (!readyToShootSecondary)
        {
            currentReloadTimeSecondary += 1 * Time.deltaTime;
        }

        if (currentReloadTimePrimary >= reloadTimePrimary)
        {
            readyToShootPrimary = true;
            currentReloadTimePrimary = 0;
        }

        if (currentReloadTimeSecondary >= reloadTimeSecondary)
        {
            readyToShootSecondary = true;
            currentReloadTimeSecondary = 0;
        }
    }

    public void ShootInput(InputAction.CallbackContext input)
    {
        if (primaryShoot)
        {
            if (readyToShootPrimary)
            {
                ShootPrimaryLogic();
            }
        }
        else
        {
            if (readyToShootSecondary)
            {
                ShootSecondaryLogic();
            }
        }
    }

    private void ShootPrimaryLogic()
    {
        readyToShootPrimary = false;

        GameObject NewBullet = Instantiate(bullet, shootShellPosition.transform);
        NewBullet.GetComponent<Rigidbody>().AddForce(shootShellPosition.forward * shootForcePrimary, ForceMode.Impulse);
    }

    private void ShootSecondaryLogic()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootSecondaryPosition.transform.position, shootSecondaryPosition.transform.forward, out hit, rangeShootSecondary))
        {
            hit.transform.GetComponent<Rigidbody>();
            hit.rigidbody.AddForce(shootSecondaryPosition.transform.forward * shootForceSecondary, ForceMode.Impulse);
        }
    }

    public void ChangeWeapon(InputAction.CallbackContext input)
    {
        primaryShoot = !primaryShoot;
    }

}
