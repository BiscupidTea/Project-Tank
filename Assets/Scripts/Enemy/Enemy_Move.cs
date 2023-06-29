using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TODO: Fix - Is this a duplicated class?
public class Enemy_Move_Patroll : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private NavMeshAgent Enemy;
    public Transform[] patrolPoints;
    public int targetPoint;
    private Vector3 target;

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        Enemy = GetComponent<NavMeshAgent>();
        //BUG: Possible nullReferenceException
        target = patrolPoints[0].position;
        UpdateDestination();

    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IncreaseTarget();
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        target = patrolPoints[targetPoint].position;
        Enemy.destination = target;
    }

    //TODO: Fix - Unclear name
    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint == patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
