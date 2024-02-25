using UnityEngine;

public class BarricadeController : MonoBehaviour, IHealthComponent
{
    [SerializeField]private float currentHealth;
    protected bool isAlive;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Start()
    {
        isAlive = true;
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    [ContextMenu("Kill Barricade")]
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
