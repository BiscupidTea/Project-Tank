using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy shoot manager
/// </summary>
public class Enemy_Shoot : MonoBehaviour
{
    [Header("GameObjects Info")]
    [SerializeField] private GameObject turret;
    [SerializeField] private Weapon cannon;

    [Header("Basic Info")]
    [SerializeField] private float viewRange;
    [SerializeField] private float shootRange;
    [SerializeField] private float rotationSpeed;

    private bool playerInViewRange;
    private bool playerInShootRange;

    private GameObject player;
    public float ShootRange { get => shootRange; set => shootRange = value; }
    public float ViewRange { get => viewRange; set => viewRange = value; }
    public bool PlayerInViewRange { get => playerInViewRange; set => playerInViewRange = value; }
    public bool PlayerInShootRange { get => playerInShootRange; set => playerInShootRange = value; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Turret");

    }
    private void Update()
    {
        var distancePlayerEnemy = Vector3.Distance(transform.position, player.transform.position);

        if (distancePlayerEnemy <= ViewRange)
        {
            PlayerInViewRange = true;
            StartCoroutine(TargetPlayer());
            if (distancePlayerEnemy <= ShootRange)
            {
                PlayerInShootRange = true;
                StartCoroutine(AttackPlayer());
            }
            else
            {
                PlayerInShootRange = false;
            }
        }
    }
    /// <summary>
    /// shoot weapon inserted
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackPlayer()
    {
        cannon.Shoot();
        yield return null;
    }
    /// <summary>
    /// rotate turret towards player
    /// </summary>
    /// <returns></returns>
    private IEnumerator TargetPlayer()
    {
        Quaternion rotTarget = Quaternion.LookRotation(player.transform.position - turret.transform.position);
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, ViewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, ShootRange);
    }
}
