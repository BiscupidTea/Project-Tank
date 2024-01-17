using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// abstract class for create a weapon
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Transform initialShootPosition;
    [SerializeField] private float reloadTime;
    [SerializeField] private int maxAmmo;
    private int currentAmmo;
    private bool isReloading;
    public event Action OnShoot;

    public Transform InitialShootPosition { get => initialShootPosition; set => initialShootPosition = value; }
    public bool IsReloading { get => isReloading; set => isReloading = value; }

    /// <summary>
    /// Event raised when ConsumeAmmo is called
    /// </summary>
    public event Action<int> OnConsumedAmmo;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    /// <summary>
    /// Shoot logic
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Set currentAmmo to maxAmmo value after a reload time 
    /// </summary>
    /// <returns></returns>
    public IEnumerator Reload()
    {
        IsReloading = true;
        Debug.Log(name + ": reloading ammo...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log(name + ": ammo reloaded; current ammo = " + currentAmmo);
        IsReloading = false;
    }

    /// <summary>
    /// Try to decreases the current ammo and raises the OnConsumedAmmo Event
    /// </summary>
    /// <param name="ammoQty">Quantity to consume</param>
    protected bool TryConsumeAmmo(int ammoQty)
    {
        bool didConsume = false;

        if (currentAmmo > 0)
        {
            OnShoot?.Invoke();
            currentAmmo -= ammoQty;

            if (OnConsumedAmmo != null)
            {
                OnConsumedAmmo(currentAmmo);
            }

            didConsume = true;
        }

        if (currentAmmo <= 0 && !IsReloading)
        {
            StartCoroutine(Reload());
        }

        return didConsume;

    }
}