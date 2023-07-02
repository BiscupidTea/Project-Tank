using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move_Patrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Enemy;
    public GameObject player;
    public Enemy_Shoot enemy_Shoot;
    public Transform[] patrolPoints;
    public int targetPoint;
    private Vector3 target;

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        target = patrolPoints[0].position;
        UpdateDestination();
        enemy_Shoot = GetComponent<Enemy_Shoot>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemy_Shoot.TargetingPlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > enemy_Shoot.ShootRange - 1)
            {
                Enemy.isStopped = false;
                Enemy.destination = player.transform.position;
            }
            else
            {
                Enemy.isStopped = true;
            }
        }
        else
        {
            Enemy.isStopped = false;
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IncreaseTarget();
                UpdateDestination();
            }
        }
    }

    private void UpdateDestination()
    {
        target = patrolPoints[targetPoint].position;
        Enemy.destination = target;
    }

    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint == patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
