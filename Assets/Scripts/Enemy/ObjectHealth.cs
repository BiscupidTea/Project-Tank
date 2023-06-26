using System;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isAlive;

    private float health;

    public event Action OnDeath;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public float Health { get => health; set => health = value; }

    private void Start()
    {
        Health = maxHealth;
        IsAlive = true;
    }
    public void ReceiveDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            IsAlive = false;
            OnDeath();
        }
    }

    [ContextMenu("Test Death")]
    private void TestDeath()
    {
        ReceiveDamage(Health);
    }
}
