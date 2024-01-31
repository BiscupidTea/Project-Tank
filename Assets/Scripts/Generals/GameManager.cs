using UnityEngine;

/// <summary>
/// Game manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerHud playerHud;

    private PlayerController player;

    private void Start()
    {
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObject.GetComponent<PlayerController>();

    }
    private void Update()
    {
        if (player.IsAlive())
        {
            if (enemyManager.EnemySpawned.Count == 0)
            {
                playerHud.SetPlayerWin();
            }
        }
        else
        {
            playerHud.SetPlayerLose();
        }

        playerHud.SetTankShow(enemyManager.EnemySpawned.Count);
    }
}
