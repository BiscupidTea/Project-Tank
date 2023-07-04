using System;
using UnityEngine;

/// <summary>
/// Weapon type - MachineGun
/// </summary>
public class MachineGunWeapon : Weapon
{
    [SerializeField] private int bulletsCosume;
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private float range;
    public event Action<Vector3> OnShootMachineGun;

    public override void Shoot()
    {
        if (TryConsumeAmmo(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(InitialShootPosition.position, InitialShootPosition.forward, out hit, range))
            {
                OnShootMachineGun?.Invoke(hit.point);

                if (hit.rigidbody)
                {
                    if (hit.collider.TryGetComponent<Health>(out var health))
                    {
                        health.ReceiveDamage(damage);
                    }
                    hit.rigidbody.AddForce(InitialShootPosition.forward * force, ForceMode.Impulse);
                }
            }
            else
            {
                Vector3 defaultPos = InitialShootPosition.position + InitialShootPosition.forward * range;
                OnShootMachineGun?.Invoke(defaultPos);
            }
        }
    }
}
