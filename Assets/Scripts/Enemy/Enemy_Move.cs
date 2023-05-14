using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField] private bool isPatrol;
    public Transform[] patrolPoints;
    public int targetPoint;
    public float moveSpeed;
    public float rotateSpeed;
    private bool isRotating;

    void Start()
    {
        isRotating = false;
        targetPoint = 0;
    }

    void Update()
    {
        if (isPatrol)
        {
            if (!isRotating)
            {
                GoToNextPoint();
            }
            else
            {
                RotateToNextPoint();
            }
        }
    }

    private void GoToNextPoint()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTarget();
            isRotating = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, moveSpeed * Time.deltaTime);
    }

    private void RotateToNextPoint()
    {
        Quaternion rotTarget = Quaternion.LookRotation(patrolPoints[targetPoint].transform.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotateSpeed * Time.deltaTime);

        if (transform.rotation == rotTarget) 
        {
            isRotating = false;
        }
    }

    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
