using System;
using System.Collections;
using UnityEngine;

//TODO: Documentation - Add summary
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float reloadTime;
    [SerializeField] private int maxAmmo;
    private int currentAmmo;
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
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }

    /// <summary>
    /// Decreases the current ammo and raises the OnConsumedAmmo Event
    /// </summary>
    /// <param name="ammoQty">Quantity to consume</param>
    protected void ConsumeAmmo(int ammoQty)
    {
        currentAmmo -= ammoQty;

        if (OnConsumedAmmo != null)
        {
            OnConsumedAmmo(currentAmmo);
        }
    }
}