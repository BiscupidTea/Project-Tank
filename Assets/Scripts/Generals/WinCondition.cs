using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private PlayerHud playerHud;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private Health player;
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

        if (player.IsAlive)
        {
            if (tanksDestroyed == 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                playerHud.SwitchCanvas(playerHud.WinCanvas, playerHud.CurrentCanvas);
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerHud.SwitchCanvas(playerHud.LoseCanvas, playerHud.CurrentCanvas);
        }
    }
}
