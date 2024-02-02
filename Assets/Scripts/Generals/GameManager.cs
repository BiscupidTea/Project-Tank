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
        player.onDeath.AddListener(PlayerDeath);
        enemyManager.winCondition.AddListener(PlayerWin);
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

        playerHud.SetTankShow(enemyManager.EnemySpawned.Count);
    }

    private void PlayerDeath(GameObject Player)
    {
        playerHud.SetPlayerLose();
        player.onDeath.RemoveListener(PlayerDeath);
    }

    private void PlayerWin(GameObject Player)
    {
        playerHud.SetPlayerWin();
        player.onDeath.RemoveListener(PlayerWin);
    }
}
