using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    [Header("GameObjects Info")]
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject shootShellPosition;

    [Header("Basic Info")]
    [SerializeField] private float viewRagnge;
    [SerializeField] private float shootRange;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool shootTurret;

    [Header("Shoot Info")]
    [SerializeField] private float damage;
    [SerializeField] private float reloadTime;
    [SerializeField] private float shootForce;

    private float timerReload;
    private bool targetingPlayer;
    private bool attackingPlayer;

    public float Damage { get => damage; set => damage = value; }
    public bool TargetingPlayer { get => targetingPlayer; set => targetingPlayer = value; }
    public float ShootRange { get => shootRange; set => shootRange = value; }

    private void Start()
    {
        if (shootTurret)
        {
            player = GameObject.FindGameObjectWithTag("Turret");
        }
    }
    private void Update()
    {
        DetectAndAttackPlayer();

        RotateTurret();
        AttackPlayer();

        timerReload += Time.deltaTime;
    }
    private void DetectAndAttackPlayer()
    {
        var distancePlayerEnemy = Vector3.Distance(transform.position, player.transform.position);
        targetingPlayer = (distancePlayerEnemy <= viewRagnge);
        attackingPlayer = (distancePlayerEnemy <= ShootRange);
    }

    private void RotateTurret()
    {
        if (targetingPlayer)
        {
            if (shootTurret)
            {
                Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - turret.transform.position);
                //TODO: Fix - Repeated code
                turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - new Vector3(0, 3, 0) - turret.transform.position);
                turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void AttackPlayer()
    {
        //TODO: TP2 - FSM
        if (attackingPlayer)
        {
            if (timerReload >= reloadTime)
            {
                GameObject NewBullet = Instantiate(shell, shootShellPosition.transform.position, shootShellPosition.transform.rotation);
                NewBullet.GetComponent<Rigidbody>().AddForce(shootShellPosition.transform.forward * shootForce, ForceMode.Impulse);
                timerReload = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, viewRagnge);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, ShootRange);

        if (player && attackingPlayer)
        {
            Gizmos.DrawLine(turret.transform.position, player.transform.position);
        }
    }
}
