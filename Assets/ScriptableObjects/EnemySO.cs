using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Base Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Basic Info")]
    public int health;
    public GameObject tankAsset;

    [Header("Phisics info")]
    public float mass;
    public Vector3 boxColliderCenter;
    public Vector3 boxColliderSize;

    [Header("Shoot Info")]
    public int viewRange;
    public int shootRange;
    public int rotationSpeed;

    [Header("Weapon Info")]
    public GameObject weapon;
}
