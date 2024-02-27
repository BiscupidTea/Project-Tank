using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerHud playerHud;

    private GameObject player;
    private PlayerController playerController;
    private bool cheats;

    private void Start()
    {
        cheats = PlayerPrefs.GetInt("cheats", 0) == 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.onDeath.AddListener(PlayerDeath);
        playerController.sendCheatCode.AddListener(ReciveCode);
        playerController.activateCheats.AddListener(SwitchCheatsState);

        enemyManager.winCondition.AddListener(PlayerWin);
    }
    private void Update()
    {
        if (playerController.IsAlive())
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
        playerController.onDeath.RemoveListener(PlayerDeath);
    }

    private void PlayerWin(GameObject Player)
    {
        playerHud.SetPlayerWin();
        playerController.onDeath.RemoveListener(PlayerWin);
    }

    private void ReciveCode(string cheatCode)
    {
        if (cheats)
        {
            switch (cheatCode)
            {
                case "NextLevel":
                    var currentScene = SceneManager.GetActiveScene();
                    sceneLoader.LoadLevel(currentScene.buildIndex + 1);
                    Debug.Log("NextLevel Activated!");
                    break;

                case "GodMode":
                    playerController.Immortal = !playerController.Immortal;
                    Debug.Log("GodMode Activated!");
                    break;

                case "Flash":
                    PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

                    if (playerMovement.CurrentSpeed == playerMovement.MaxSpeed)
                    {
                        playerMovement.CurrentSpeed = playerMovement.MaxSpeed * 5;
                    }
                    else
                    {
                        playerMovement.CurrentSpeed = playerMovement.MaxSpeed;
                    }
                    Debug.Log("Flash Activated!");
                    break;

                case "Nuke":
                    enemyManager.ClearAllEnemies();
                    Debug.Log("Nuke Activated!");
                    break;
            }
        }
    }

    private void SwitchCheatsState(GameObject gameObject)
    {
        cheats = !cheats;
        PlayerPrefs.SetInt("cheats", cheats ? 1 : 0);
        PlayerPrefs.Save();
    }
}
