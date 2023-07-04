using System;
using UnityEngine;

/// <summary>
/// Weapon type - CannonWeapon
/// </summary>
public class CannonWeapon : Weapon
{
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private ShellLogic PrefabShell;
    public event Action OnShootCannon;
    public override void Shoot()
    {
        if (TryConsumeAmmo(1))
        {
            ShellLogic NewBullet = Instantiate(PrefabShell, InitialShootPosition.position, InitialShootPosition.rotation);
            NewBullet.Damage = damage;
            NewBullet.GetComponent<Rigidbody>().AddForce(InitialShootPosition.forward * force, ForceMode.Impulse);

            OnShootCannon?.Invoke();
        }
    }
}