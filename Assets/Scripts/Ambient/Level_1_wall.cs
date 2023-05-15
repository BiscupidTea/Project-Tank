using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level_1_wall : MonoBehaviour
{
    [SerializeField] private GameObject[] tanks;
    private int tanksDestroyed;

    private void Start()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanksDestroyed++;
        }
    }
    private void Update()
    {
        tanksDestroyed = 0;
        for (int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i] != null)
            {
                tanksDestroyed++;
            }
        }

        if (tanksDestroyed == 0)
        {
            Destroy(gameObject);
        }

    }
}
