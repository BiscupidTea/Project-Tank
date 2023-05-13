using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    [Header("GameObjects Info")]
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Shell;
    [SerializeField] private GameObject shootShellPosition;

    [Header("Basic Info")]
    [SerializeField] private float ViewRagnge;
    [SerializeField] private float ShootRange;
    [SerializeField] private float RotationSpeed;

    [Header("Shoot Info")]
    [SerializeField] private float Damage;
    [SerializeField] private float ReloadTime;
    [SerializeField] private float shootForce;

    private float timerReload;
    private bool TargetingPlayer;
    private bool AttackingPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Turret");
    }
    private void Update()
    {
        DetectViewPlayer();
        DetectAttackPlayer();

        RotateTurret();
        AttackPlayer();

        timerReload += Time.deltaTime;
    }
    private void DetectViewPlayer()
    {
        var distancePlayerEnemy = Vector3.Distance(transform.position, player.transform.position);
        if (distancePlayerEnemy <= ViewRagnge)
        {
            TargetingPlayer = true;
        }
        else
        {
            TargetingPlayer = false;
        }
    }

    private void DetectAttackPlayer()
    {
        var distancePlayerEnemy = Vector3.Distance(transform.position, player.transform.position);
        if (distancePlayerEnemy <= ShootRange)
        {
            AttackingPlayer = true;
        }
        else
        {
            AttackingPlayer = false;
        }
    }

    private void RotateTurret()
    {
        if (TargetingPlayer)
        {
            Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - turret.transform.position);

            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, rotTarget, RotationSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        if (AttackingPlayer)
        {
            if (timerReload >= ReloadTime)
            {
                GameObject NewBullet = Instantiate(Shell, shootShellPosition.transform);
                NewBullet.GetComponent<Rigidbody>().AddForce(shootShellPosition.transform.forward * shootForce, ForceMode.Impulse);
                timerReload = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, ViewRagnge);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, ShootRange);

        if (player)
        {
            Gizmos.DrawLine(turret.transform.position, player.transform.position);
        }
    }

    public float GetDamage()
    {
        return Damage;
    }
}
