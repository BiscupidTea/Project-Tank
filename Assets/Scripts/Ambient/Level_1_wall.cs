using UnityEngine;

/// <summary>
/// level 1 wall that destroys when enemy are destroyed
/// </summary>
public class Level_1_wall : MonoBehaviour
{
    [SerializeField] private Health[] tanks;
    private int tanksQuantity;

    private void OnEnable()
    {
        foreach (var tank in tanks) 
        {
            tank.OnDeath += HandleTankDeath;
        }
    }
    private void Start()
    {
        tanksQuantity = tanks.Length;
    }
    private void HandleTankDeath()
    {
        tanksQuantity--;

        if (tanksQuantity == 0)
        {
            foreach (var tank in tanks)
            {
                tank.OnDeath -= HandleTankDeath;
            }
            Destroy(gameObject);
        }
    }
}
