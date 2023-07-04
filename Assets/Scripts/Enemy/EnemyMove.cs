using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy move manager whit navMesh
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Enemy;
    [SerializeField] private Transform[] patrolPoints;
    private int targetPoint;
    private Vector3 target;
    private Enemy_Shoot enemyShoot;
    private GameObject player;
    private bool isStatic;

    void Awake()
    {
        Enemy = GetComponent<NavMeshAgent>();
        enemyShoot = GetComponent<Enemy_Shoot>();
        player = GameObject.FindGameObjectWithTag("Turret");

        if (patrolPoints.Length != 0)
        {
            targetPoint = 0;
            isStatic = false;
        }
        else
        {
            Enemy.isStopped = true;
            isStatic = true;
        }

    }

    void Update()
    {
        if (enemyShoot.PlayerInViewRange)
        {
            if (!Enemy.isStopped)
            {
                StopCoroutine(UpdateDestination());
                Enemy.destination = player.transform.position;
            }
            if (!enemyShoot.PlayerInShootRange)
            {
                Enemy.isStopped = false;
                Enemy.destination = player.transform.position;
            }
            else
            {
                Enemy.isStopped = true;
            }
        }
        else if (!isStatic)
        {
            StartCoroutine(UpdateDestination());
        }

    }

    /// <summary>
    /// Change destination for enemy
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateDestination()
    {

        if (Enemy.remainingDistance < 1)
        {
            targetPoint++;
            if (targetPoint == patrolPoints.Length)
            {
                targetPoint = 0;
            }
        }
        target = patrolPoints[targetPoint].position;
        Enemy.destination = target;

        yield return null;
    }
}
