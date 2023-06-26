using UnityEngine;

[RequireComponent(typeof(ObjectHealth))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ObjectHealth health;

    private void Awake()
    {
        health = GetComponent<ObjectHealth>();
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
