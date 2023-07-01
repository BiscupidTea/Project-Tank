using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private TrailRenderer PrefabBulletTrail;
    public override void Shoot()
    {
        RaycastHit hit;
        TrailRenderer trail = Instantiate(PrefabBulletTrail, InitialShootPosition.position, Quaternion.identity);

        if (Physics.Raycast(InitialShootPosition.position, InitialShootPosition.forward, out hit, range))
        {
            StartCoroutine(SpawnTrail(trail, hit.point));

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
            StartCoroutine(SpawnTrail(trail, defaultPos));
        }
        ConsumeAmmo(1);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 EndPosition)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, EndPosition, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = EndPosition;

        Destroy(trail.gameObject, trail.time);
    }
}
