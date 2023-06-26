using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootShellPosition;
    [SerializeField] Transform shootSecondaryPosition;

    [Header("Primary Shoot")]
    [SerializeField] private Weapon primaryWeapon;
    //TODO create weaponVFX and listen to onShoot event
    [Obsolete][SerializeField] private GameObject AnimationPrimaryShoot;
    [Obsolete][SerializeField] private float AnimationPrimaryShootTime;
    [Obsolete]private float animationTimePrimary;
    [Obsolete]private bool animationReadyPrimary = false;

    [Header("Secondary Shoot")]
    [SerializeField] private Weapon secondaryWeapon;
    [SerializeField] private float rangeShootSecondary;
    [SerializeField] private float shootForceSecondary;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private GameObject AnimationSecondaryShoot;
    [SerializeField] private float AnimationSecondaryShootTime;

    [Header("Sounds Shoot")]
    [SerializeField] private AudioClip shootanimation1;
    [SerializeField] private AudioClip shootanimation2;
    private float timeAudio;
    private float currentTimeAudio;

    private float animationTimeSecondary;
    private bool animationReadySecondary = false;

    private bool primaryShoot;
    private float currentReloadTimePrimary;
    private float currentReloadTimeSecondary;

    private bool readyToShootPrimary;
    private bool readyToShootSecondary;

    private void Awake()
    {
        timeAudio = shootanimation2.length;
        currentTimeAudio = timeAudio;

        primaryShoot = true;

        readyToShootPrimary = true;
        readyToShootSecondary = false;

        animationTimePrimary = 0;
        animationTimeSecondary = 0;

        AnimationPrimaryShoot.SetActive(false);
        AnimationSecondaryShoot.SetActive(false);
    }

    private void Update()
    {
        Reloaders();

        Animations();

        currentTimeAudio += Time.deltaTime;
    }

    private void Animations()
    {
        if (animationReadyPrimary)
        {
            animationTimePrimary += 1 * Time.deltaTime;
        }

        if (animationReadySecondary)
        {
            animationTimeSecondary += 1 * Time.deltaTime;
        }

        if (animationTimePrimary > AnimationPrimaryShootTime)
        {
            animationReadyPrimary = false;
            AnimationPrimaryShoot.SetActive(false);
            animationTimePrimary = 0;
        }

        if (animationTimeSecondary > AnimationSecondaryShootTime)
        {
            animationReadySecondary = false;
            AnimationSecondaryShoot.SetActive(false);
            animationTimeSecondary = 0;
        }
    }

    private void Reloaders()
    {
        if (!readyToShootPrimary)
        {
            currentReloadTimePrimary += 1 * Time.deltaTime;
        }

        if (!readyToShootSecondary)
        {
            currentReloadTimeSecondary += 1 * Time.deltaTime;
        }

        if (currentReloadTimePrimary > reloadTimePrimary)
        {
            readyToShootPrimary = true;
            currentReloadTimePrimary = 0;
        }

        if (currentReloadTimeSecondary > reloadTimeSecondary)
        {
            readyToShootSecondary = true;
            currentReloadTimeSecondary = 0;
        }
    }

    public void ShootInput(InputAction.CallbackContext input)
    {
        if (primaryShoot)
        {
            if (readyToShootPrimary)
            {
                ShootPrimaryLogic();
            }
        }
        else
        {
            if (readyToShootSecondary)
            {
                ShootSecondaryLogic();
            }
        }
    }

    private void ShootPrimaryLogic()
    {
        readyToShootPrimary = false;

        AnimationPrimaryShoot.SetActive(true);
        animationReadyPrimary = true;

        GameObject NewBullet = Instantiate(bullet, shootShellPosition.transform.position, shootShellPosition.transform.rotation);
        NewBullet.GetComponent<Rigidbody>().AddForce(shootShellPosition.forward * shootForcePrimary, ForceMode.Impulse);

        SoundManager.Instance.PlaySound(shootanimation1);
    }

    private void ShootSecondaryLogic()
    {
        AnimationSecondaryShoot.SetActive(true);
        animationReadySecondary = true;

        RaycastHit hit;
        TrailRenderer trail = Instantiate(bulletTrail, shootSecondaryPosition.transform.position, Quaternion.identity);

        if (Physics.Raycast(shootSecondaryPosition.transform.position, shootSecondaryPosition.transform.forward, out hit, rangeShootSecondary))
        {
            StartCoroutine(SpawnTrail(trail, hit.point));

            if (hit.rigidbody)
            {
                Debug.Log(hit);
                if (hit.collider.GetComponent<ObjectHealth>())
                {
                    hit.collider.GetComponent<ObjectHealth>().ReceiveDamage(damageScondary);
                }
                hit.rigidbody.AddForce(shootSecondaryPosition.transform.forward * shootForceSecondary, ForceMode.Impulse);
            }
        }
        else
        {
            Vector3 defaultPos = shootSecondaryPosition.transform.position + shootSecondaryPosition.transform.forward * rangeShootSecondary;
            StartCoroutine(SpawnTrail(trail, defaultPos));
        }

        if (currentTimeAudio >= timeAudio)
        {
            SoundManager.Instance.PlaySound(shootanimation2, 0.4f);
            currentTimeAudio = 0;
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

    public void ChangeWeapon(InputAction.CallbackContext input)
    {
        primaryShoot = !primaryShoot;
    }

    public float GetPrimaryDamage()
    {
        return damagePrimary;
    }

    public float GetSecondaryDamage()
    {
        return damageScondary;
    }

    public bool WeaponUsing()
    {
        return primaryShoot;
    }

    public bool GetPrimaryReadyToShoot()
    {
        return readyToShootPrimary;
    }

    public bool GetSecondaryReadyToShoot()
    {
        return readyToShootSecondary;
    }
}