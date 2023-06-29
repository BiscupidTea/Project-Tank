using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeapon : Weapon
{
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private Transform initialShootPosition;
    [SerializeField] private TrailRenderer PrefabBulletTrail;
    public override void Shoot()
    {
        RaycastHit hit;
        TrailRenderer trail = Instantiate(PrefabBulletTrail, initialShootPosition.position, Quaternion.identity);

        if (Physics.Raycast(initialShootPosition.position, initialShootPosition.forward, out hit, range))
        {
            StartCoroutine(SpawnTrail(trail, hit.point));

            if (hit.rigidbody)
            {
                //TODO: Fix - Bad log/Log out of context
                Debug.Log(hit);
                //TODO: Fix - TryGetComponent
                if (hit.collider.GetComponent<ObjectHealth>())
                {
                    hit.collider.GetComponent<ObjectHealth>().ReceiveDamage(damage);
                }
                hit.rigidbody.AddForce(initialShootPosition.forward * force, ForceMode.Impulse);
            }
        }
        else
        {
            Vector3 defaultPos = initialShootPosition.position + initialShootPosition.forward * range;
            StartCoroutine(SpawnTrail(trail, defaultPos));
        }
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
