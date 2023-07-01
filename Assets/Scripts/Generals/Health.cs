using System;
using UnityEngine;

//TODO: Documentation - Add summary
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isAlive;

    private float currentHealth;

    public event Action OnDeath;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        IsAlive = true;
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
            IsAlive = false;
            OnDeath();
        }
    }

    /// <summary>
    /// kill test for debug mode
    /// </summary>
    [ContextMenu("Test Death")]
    private void TestDeath()
    {
        ReceiveDamage(CurrentHealth);
    }
}
