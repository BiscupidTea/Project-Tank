using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float Maxhealth;
    [SerializeField] private bool isAlive;

    private float health;

    private void Start()
    {
        health = Maxhealth;
        isAlive = true;
    }
    private void Update()
    {
        if (health <= 0)
        {
            isAlive = false;
        }
    }
    public void ReciveDamage(float damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public float GetMaxHealth()
    {
        return Maxhealth;
    }
}
