using UnityEngine;

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
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
