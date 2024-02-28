using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy shoot manager
/// </summary>
public class EnemyTankShoot : MonoBehaviour
{
    [Header("GameObjects Info")]
    [SerializeField] private Transform assetTurret;
    [SerializeField] private Transform assetCannon;
    [SerializeField] private Weapon cannon;

    [Header("Basic Info")]
    [SerializeField] private float viewRange;
    [SerializeField] private float shootRange;
    [SerializeField] private float turretRotationSpeed;
    [SerializeField] private float cannonRotationSpeed;
    [SerializeField] private float cannonMaxRotation;
    [SerializeField] private float cannonMinRotation;

    private bool playerInViewRange;
    private bool playerInShootRange;

    private GameObject player;
    public float ShootRange { get => shootRange; set => shootRange = value; }
    public float ViewRange { get => viewRange; set => viewRange = value; }
    public bool PlayerInViewRange { get => playerInViewRange; set => playerInViewRange = value; }
    public bool PlayerInShootRange { get => playerInShootRange; set => playerInShootRange = value; }
    public Transform AssetTurret { get => assetTurret; set => assetTurret = value; }
    public Transform AssetCannon { get => assetCannon; set => assetCannon = value; }
    public Weapon Cannon { get => cannon; set => cannon = value; }
    public float TurretRotationSpeed { get => turretRotationSpeed; set => turretRotationSpeed = value; }
    public float CannonRotationSpeed { get => cannonRotationSpeed; set => cannonRotationSpeed = value; }
    public float CannonMaxRotation { get => cannonMaxRotation; set => cannonMaxRotation = value; }
    public float CannonMinRotation { get => cannonMinRotation; set => cannonMinRotation = value; }
    public GameObject Player { get => player; set => player = value; }

    private void Update()
    {
        var distancePlayerEnemy = Vector3.Distance(transform.position, Player.transform.position);

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
        Cannon.Shoot();
        yield return null;
    }
    /// <summary>
    /// rotate turret towards player
    /// </summary>
    /// <returns></returns>
    private IEnumerator TargetPlayer()
    {
        RotateTurret();
        RotateCannon();
        yield return null;
    }


    private void RotateTurret()
    {
        Vector3 direction = Player.transform.position - assetTurret.position;
        direction.y = 0f;

        Quaternion finalRotation = Quaternion.LookRotation(direction);

        Vector3 eulerRotation = finalRotation.eulerAngles;
        eulerRotation.x = 0f;
        eulerRotation.z = 0f;

        assetTurret.rotation = Quaternion.RotateTowards(assetTurret.rotation, Quaternion.Euler(eulerRotation), turretRotationSpeed * Time.deltaTime);
    }
    private void RotateCannon()
    {
        Vector3 direccion = Player.transform.position - assetCannon.position;

        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);

        float rotacionX = Mathf.Clamp(rotacionDeseada.eulerAngles.x, cannonMinRotation, cannonMaxRotation);

        assetCannon.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, ViewRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, ShootRange);
    }
}
