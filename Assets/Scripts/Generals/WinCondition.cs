using System.Collections;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private Health player;
    private int tanksDestroyed;
    public bool playerWin;
    public bool playerLose;

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

        if (player.IsAlive)
        {
            if (tanksDestroyed == 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                playerWin = true;
                playerLose = false;
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerLose = true;
            playerWin = false;
        }
    }
}
