using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Tank/Enemy")]
public class EnemyTankSO : EnemySO
{
    [Header("Phisics Info")]
    public float mass;

    [Header("Shoot Info")]
    public int viewRange;
    public int shootRange;
    public float turretRotationSpeed;
    public float cannonRotationSpeed;
    public float cannonMaxRotation;
    public float cannonMinRotation;

    [Header("Cannon Weapon Info")]
    public GameObject weapon;
    public float damage;
    public float force;
}