using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool isAlive;

    private void Start()
    {
        isAlive = true;
    }
    private void Update()
    {
        if (health <= 0)
        {
            isAlive = false;
        }
    }
    public void GetDamage(float damage)
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
}
