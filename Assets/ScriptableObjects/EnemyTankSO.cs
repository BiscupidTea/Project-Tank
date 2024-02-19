using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Tank/Enemy")]
public class EnemyTankSO : EnemySO
{
    [Header("Phisics Info")]
    public float mass;

    [Header("Shoot Info")]
    public int viewRange;
    public int shootRange;
    public int rotationSpeed;

    [Header("Cannon Weapon Info")]
    public GameObject weapon;
}