using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
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
            _sceneLoader.Level2();
        }

    }
}
