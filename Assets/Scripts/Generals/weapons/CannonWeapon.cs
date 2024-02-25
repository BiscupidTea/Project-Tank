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

    public float Damage { get => damage; set => damage = value; }
    public float Force { get => force; set => force = value; }

    public event Action OnShootCannon;
    public override void Shoot()
    {
        if (TryConsumeAmmo(1))
        {
            ShellLogic NewBullet = Instantiate(PrefabShell, InitialShootPosition.position, InitialShootPosition.rotation);
            NewBullet.Damage = Damage;
            NewBullet.GetComponent<Rigidbody>().AddForce(InitialShootPosition.forward * Force, ForceMode.Impulse);

            OnShootCannon?.Invoke();
        }
    }
}