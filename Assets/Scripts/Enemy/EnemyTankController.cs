using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enemy state Manager
/// </summary>
public class EnemyTankController : MonoBehaviour, IHealthComponent
{
    [SerializeField] public UnityEvent<GameObject> onDeath;
    [SerializeField] public UnityEvent<float> onTakeDamage;
    private string id;
    private float currentHealth;
    protected bool isAlive;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public string Id { get => id; set => id = value; }

    public virtual void Death()
    {
        onDeath.Invoke(this.gameObject);
    }

    [ContextMenu("Kill Enemy")]
    public void KillEnemy()
    {
        currentHealth = 0;
        isAlive = false;
        Death();
    }

    /// <summary>
    /// Decreases current health taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
        onTakeDamage.Invoke(currentHealth);

        if (CurrentHealth <= 0)
        {
            isAlive = false;
            Death();
        }
    }

    public virtual bool IsAlive()
    {
        return isAlive;
    }

    public void SetIsAlive(bool status)
    {
        isAlive = status;
    }
}
