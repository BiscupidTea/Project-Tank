using System.Collections;
using UnityEngine;

/// <summary>
/// MachineGun Visual effect manager
/// </summary>
public class MachineGunVFX : MonoBehaviour
{
    [SerializeField] private MachineGunWeapon machineGun;
    [SerializeField] private ParticleSystem effectParticle;
    [SerializeField] private TrailRenderer PrefabBulletTrail;
    private void Awake()
    {
        machineGun.OnShootMachineGun += OnAction;
    }

    private void OnDestroy()
    {
        machineGun.OnShootMachineGun -= OnAction;
    }

    private void OnAction(Vector3 finalPosition)
    {
        TrailRenderer trail = Instantiate(PrefabBulletTrail, machineGun.InitialShootPosition.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, finalPosition));
        effectParticle.Play();
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
