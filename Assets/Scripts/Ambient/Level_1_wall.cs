using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TODO: Documentation - Add summary
public class Level_1_wall : MonoBehaviour
{
    [SerializeField] private GameObject[] tanks;
    private int tanksDestroyed;

    private void Start()
    {
        //TODO: Fix - tanksDestroyed = tanks.Length - Why is this necessary?
        for (int i = 0; i < tanks.Length; i++)
        {
            tanksDestroyed++;
        }
    }
    private void Update()
    {
        //TODO: Fix - Should be event based
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
