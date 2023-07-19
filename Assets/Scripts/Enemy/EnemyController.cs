using UnityEngine;

/// <summary>
/// Enemy state Manager
/// </summary>
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        health.OnDeath -= HandleDeath;
    }
    /// <summary>
    /// kill enemy by debug
    /// </summary>
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
