using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy move manager whit navMesh
/// </summary>
public class EnemyTankMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] public Transform[] patrolPoints;
    private int targetPoint;
    private Vector3 target;
    private EnemyTankShoot enemyShoot;
    private GameObject player;
    private bool isStatic;

    public NavMeshAgent Enemy { get => enemy; set => enemy = value; }
    public GameObject Player { get => player; set => player = value; }

    private void Start()
    {
        if (patrolPoints.Length != 0)
        {
            targetPoint = 0;
            isStatic = false;
            Enemy.destination = patrolPoints[0].position;
        }
        else
        {
            enemy.isStopped = true;
            isStatic = true;
        }
    }

    private void OnEnable()
    {
        Enemy = GetComponent<NavMeshAgent>();
        enemyShoot = GetComponent<EnemyTankShoot>();
    }

    void Update()
    {
        if (enemyShoot.PlayerInViewRange)
        {
            if (!Enemy.isStopped)
            {
                StopCoroutine(UpdateDestination());
                Enemy.destination = Player.transform.position;
            }
            if (!enemyShoot.PlayerInShootRange)
            {
                Enemy.isStopped = false;
                Enemy.destination = Player.transform.position;
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

        if (Enemy.remainingDistance < Enemy.stoppingDistance)
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
