using UnityEngine;

/// <summary>
/// Game manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private EnemyManager enemyManager;

    private PlayerController player;
    private int tanksDestroyed;
    public bool playerWin;
    public bool playerLose;

    private void Start()
    {
        tanks = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObject.GetComponent<PlayerController>();

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

        if (player.IsAlive())
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
