using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Base Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Basic Info")]
    public int health;
    public GameObject Asset;

    [Header("Phisics info")]
    public Vector3 boxColliderCenter;
    public Vector3 boxColliderSize;
}