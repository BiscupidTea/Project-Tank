using UnityEngine;


/// <summary>
/// 
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
            Destroy(gameObject);
        }
    }
}
