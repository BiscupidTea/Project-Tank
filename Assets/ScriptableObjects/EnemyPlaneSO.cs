using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Plane/Enemy")]
public class EnemyPlaneSO : EnemySO
{
    [Header("Movement Info")]
    public float minDistancePatrolPoints;
    public float minDistancePlayer;
    public float movementSpeed;
    public float rotationSpeed;

    [Header("Shoot Info")]
    public int viewRange;
    public int attackDelay;
    public int attackPlayerDistance;

    [Header("Cannon Weapon Info")]
    public GameObject weapon;
}