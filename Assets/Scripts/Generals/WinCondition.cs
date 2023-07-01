using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Canvas WinScreen;
    [SerializeField] private Canvas LoseScreen;
    [SerializeField] private Canvas UI;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private Health player;
    private int tanksDestroyed;

    private void Start()
    {
        UI.enabled = true;
        LoseScreen.enabled = false;
        WinScreen.enabled = false;
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

        if (player.IsAlive)
        {
            if (tanksDestroyed == 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                UI.enabled = false;
                WinScreen.enabled = true;
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UI.enabled = false;
            LoseScreen.enabled = true;
        }

        if (WinScreen.enabled == true)
        {
            UI.enabled = false;
        }
        else if (LoseScreen.enabled == true)
        {
            UI.enabled = false;
        }
    }
}
