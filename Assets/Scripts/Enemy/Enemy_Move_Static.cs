using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Move_Static : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    //TODO: TP2 - Syntax - Consistency in naming convention
    public Enemy_Shoot enemy_Shoot;
    public GameObject player;

    public bool isRotating;

    private void Start()
    {
        isRotating = true;
        enemy_Shoot = GetComponent<Enemy_Shoot>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (enemy_Shoot.TargetingPlayer)
        {
            if (isRotating)
            {
                RotateEnemy();
            }
            else
            {
                MoveEnemy();
            }
        }
    }

    private void RotateEnemy()
    {
        Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotateSpeed * Time.deltaTime);

        isRotating = (transform.rotation == rotTarget);
    }
    private void MoveEnemy()
    {
        //TODO: Fix - Repeated code
        Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) > enemy_Shoot.ShootRange - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

        if (transform.rotation != rotTarget)
        {
            isRotating = true;
        }
    }
}
