using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// player hud manager
/// </summary>
public class PlayerHud : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstButtonWin;
    [SerializeField] private GameObject firstButtonLose;

    [Header("Canvas Info")]
    [SerializeField] private CanvasGroup winCanvas;
    [SerializeField] private CanvasGroup loseCanvas;
    [SerializeField] private CanvasGroup UiCanvas;
    [SerializeField] private CanvasGroup pauseCanvas;
    [SerializeField] private CanvasGroup currentCanvas;

    [Header("Aim Info")]
    private PlayerCamera playerCamera;
    [SerializeField] private GameObject cross;

    [Header("Player Info")]
    [SerializeField] private Slider healthSliderPlayer;
    private PlayerController playerController;

    [Header("HealthBar Boss Info")]
    [SerializeField] private TextMeshProUGUI healthBossNumber;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject healthGameObjectBoss;
    [SerializeField] private Slider healthSliderBoss;

    [Header("Weapons Info")]
    [SerializeField] private GameObject[] weaponImageGameObject;
    private PlayerShoot playerShoot;
    private Image[] weaponImage;

    [Header("TankRemaing Info")]
    [SerializeField] private Image tankSprite;
    [SerializeField] private TextMeshProUGUI tankInfo;

    [Header("Pause Info")]
    [SerializeField] private PauseSystem pause;

    public CanvasGroup WinCanvas { get => winCanvas; set => winCanvas = value; }
    public CanvasGroup LoseCanvas { get => loseCanvas; set => loseCanvas = value; }
    public CanvasGroup UICanvas { get => UiCanvas; set => UiCanvas = value; }
    public CanvasGroup PauseCanvas { get => pauseCanvas; set => pauseCanvas = value; }
    public CanvasGroup CurrentCanvas { get => currentCanvas; set => currentCanvas = value; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.GetComponent<PlayerCamera>();
        playerShoot = player.GetComponent<PlayerShoot>();
        playerController = player.GetComponent<PlayerController>();

        CurrentCanvas = UICanvas;

        healthSliderPlayer.maxValue = playerController.CurrentHealth;
        healthSliderPlayer.value = playerController.CurrentHealth;

        DisableCanvas(loseCanvas);
        DisableCanvas(winCanvas);
        DisableCanvas(pauseCanvas);
        EnableCanvas(UICanvas);

        weaponImage = new Image[weaponImageGameObject.Length];
        for (int i = 0; i < weaponImageGameObject.Length; i++)
        {
            weaponImage[i] = weaponImageGameObject[i].GetComponent<Image>();
        }
        weaponImage[0].color = Color.yellow;


        if (isBoss)
        {
            tankSprite.enabled = false;
            tankInfo.enabled = false;

            healthSliderBoss.enabled = true;
            healthGameObjectBoss.SetActive(true);

            GameObject[] enemies = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject enemy in enemies)
            {
                EnemyTankController component = enemy.GetComponent<EnemyTankController>();

                if (component != null)
                {
                    boss = enemy;
                    component.onTakeDamage.AddListener(SetBossHealthBar);
                    healthSliderBoss.maxValue = component.CurrentHealth;
                    healthSliderBoss.value = component.CurrentHealth;
                }
            }

        }
        else
        {
            tankSprite.enabled = true;
            tankInfo.enabled = true;

            healthSliderBoss.enabled = false;
            healthGameObjectBoss.SetActive(false);
        }

        pause.onPause.AddListener(SwitchPause);
        playerController.onTakeDamage.AddListener(SetPlayerHealthBar);
        playerController.onAim.AddListener(SetAim);
        playerController.onSwitchWeapon.AddListener(SetWeaponSelect);
    }

    private void OnDestroy()
    {
        pause.onPause.RemoveListener(SwitchPause);
        playerController.onTakeDamage.RemoveListener(SetPlayerHealthBar);
        playerController.onAim.RemoveListener(SetAim);
        playerController.onSwitchWeapon.RemoveListener(SetWeaponSelect);
    }

    private void SwitchPause(GameObject pause)
    {
        if (currentCanvas != pauseCanvas)
        {
            SwitchCanvas(PauseCanvas, CurrentCanvas);

        }
        else
        {
            SwitchCanvas(UICanvas, CurrentCanvas);
        }
    }

    private void SetAim(GameObject playerController)
    {
        if (!cross.active)
        {
            cross.SetActive(true);
        }
        else
        {
            cross.SetActive(false);
        }
    }
    private void SetPlayerHealthBar(float playerController)
    {
        healthSliderPlayer.value = playerController;
    }

    private void SetBossHealthBar(float bossLife)
    {
        healthSliderBoss.value = bossLife;
    }

    public void SetPlayerWin()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SwitchCanvas(winCanvas, UICanvas);
        eventSystem.SetSelectedGameObject(firstButtonWin);
        Time.timeScale = 0;
    }

    public void SetPlayerLose()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SwitchCanvas(loseCanvas, UICanvas);
        eventSystem.SetSelectedGameObject(firstButtonLose);
        Time.timeScale = 0;
    }
    private void SetWeaponSelect(GameObject playerController)
    {
        for (int i = 0; i < weaponImageGameObject.Length; i++)
        {
            if (playerShoot.WeaponInUse.id == weaponImageGameObject[i].name)
            {
                weaponImage[i].color = Color.yellow;
            }
            else
            {
                weaponImage[i].color = Color.black;
            }
        }
    }

    public void SetTankShow(int totalTanks)
    {
        tankInfo.text = totalTanks.ToString();
    }

    private void SwitchCanvas(CanvasGroup canvasEnable, CanvasGroup canvasDisable)
    {
        EnableCanvas(canvasEnable);
        DisableCanvas(canvasDisable);
        CurrentCanvas = canvasEnable;
        Debug.Log("Change canvas form: " + canvasDisable + " to " + canvasEnable);
    }

    public void SetFirstButton(GameObject firstButton)
    {
        eventSystem.SetSelectedGameObject(firstButton);
        Debug.Log(eventSystem.currentSelectedGameObject);
    }

    public void DisableCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void EnableCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}
