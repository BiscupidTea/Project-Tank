using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float helath;
    [SerializeField] private bool isAlive;

    private void Update()
    {
        if (helath <= 0)
        {
            isAlive = false;
        }

        if (!isAlive ) 
        { 
            Destroy(gameObject);
        }
    }
    public void GetDamage(float damage)
    {
        helath -= damage;
    }
}
