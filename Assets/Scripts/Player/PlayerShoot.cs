using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] Camera _camera;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootShellPosition;
    [SerializeField] Transform shootMachineGunPosition;

    [SerializeField] private float shootForce;
    [SerializeField] private float reloadTime;
    private float currentReloadTime;

    private bool readyToShoot;

    private void Awake()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        if (!readyToShoot)
        {
            currentReloadTime += 1 * Time.deltaTime;
        }

        if (currentReloadTime >= reloadTime)
        {
            readyToShoot = true;
            currentReloadTime = 0;
            Debug.Log("ready!");
        }
    }

    public void ShootInput(InputAction.CallbackContext input)
    {
        if (readyToShoot)
        {
            ShootLogic();
        }
        else
        {
            Debug.Log("reloading!");
        }
    }

    private void ShootLogic()
    {
        readyToShoot = false;

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        Debug.Log("shoot");
    }

}
