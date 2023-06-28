using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : Weapon
{
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private ShellLogic PrefabShell;
    public override void Shoot()
    {
        ShellLogic NewBullet = Instantiate(PrefabShell, InitialShootPosition.position, InitialShootPosition.rotation);
        NewBullet.Damage = damage;
        NewBullet.GetComponent<Rigidbody>().AddForce(InitialShootPosition.forward * force, ForceMode.Impulse);
    }
}
