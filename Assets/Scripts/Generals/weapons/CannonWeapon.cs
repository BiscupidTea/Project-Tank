using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : Weapon
{
    //TODO: TP2 - Remove unused methods/variables/classes
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private Transform initialShootPosition;
    [SerializeField] private ShellLogic PrefabShell;
    public override void Shoot()
    {
        ShellLogic NewBullet = Instantiate(PrefabShell, initialShootPosition.position, initialShootPosition.rotation);
        //NewBullet.Damage = damage;
        NewBullet.GetComponent<Rigidbody>().AddForce(initialShootPosition.forward * force, ForceMode.Impulse);
    }
}
