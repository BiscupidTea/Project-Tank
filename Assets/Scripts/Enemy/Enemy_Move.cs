using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField] private bool isPatroling;
    [SerializeField] private NavMeshAgent Enemy;
    public Transform[] patrolPoints;
    public int targetPoint;
    private Vector3 target;

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();

        if (isPatroling)
        {
            target = patrolPoints[0].position;
            UpdateDestination();
        }
    }

    void Update()
    {
        if (isPatroling)
        {
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
