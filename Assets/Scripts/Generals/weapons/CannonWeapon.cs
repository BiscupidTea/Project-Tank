using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : Weapon
{
    //TODO: TP2 - Remove unused methods/variables/classes
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
