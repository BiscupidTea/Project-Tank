using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private WinCondition winCondition;

    [Header("Canvas Info")]
    [SerializeField] private CanvasGroup winCanvas;
    [SerializeField] private CanvasGroup loseCanvas;
    [SerializeField] private CanvasGroup UiCanvas;
    [SerializeField] private CanvasGroup pauseCanvas;
    [SerializeField] private CanvasGroup currentCanvas;

    [Header("Aim Info")]
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameObject cross;

    [Header("HealthBar Player Info")]
    [SerializeField] private Health healthPlayer;
    [SerializeField] private Health healthBoss;
    [SerializeField] private Slider healthSliderPlayer;

    [Header("HealthBar Boss Info")]
    [SerializeField] private TextMeshProUGUI healthBossNumber;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject healthGameObjectBoss;
    [SerializeField] private Slider healthSliderBoss;

    [Header("Weapons Info")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Image[] weaponImage;

    [Header("TankRemaing Info")]
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private Image tankSprite;
    [SerializeField] private TextMeshProUGUI tankInfo;
    private int totalTanks;

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
        healthPlayer = player.GetComponent<Health>();

        CurrentCanvas = UICanvas;

        healthSliderPlayer.maxValue = healthPlayer.CurrentHealth;
        healthSliderPlayer.value = healthPlayer.CurrentHealth;

        DisableCanvas(loseCanvas);
        DisableCanvas(winCanvas);
        DisableCanvas(pauseCanvas);
        EnableCanvas(UICanvas);

        if (isBoss)
        {
            tankSprite.enabled = false;
            tankInfo.enabled = false;

            healthSliderBoss.enabled = true;
            healthGameObjectBoss.SetActive(true);
            healthSliderBoss.maxValue = healthBoss.CurrentHealth;
            healthSliderBoss.value = healthBoss.CurrentHealth;
        }
        else
        {
            tankSprite.enabled = true;
            tankInfo.enabled = true;

            healthSliderBoss.enabled = false;
            healthGameObjectBoss.SetActive(false);
        }

        for (int i = 0; i < tanks.Length; i++)
        {
            totalTanks++;
        }
    }
    void Update()
    {
        SetAim();

        SetHealthBar();

        SetWeaponSelect();

        SetTankShow();

        if (pause.IsPause)
        {
            SwitchCanvas(PauseCanvas, CurrentCanvas);
        }
        else
        {
            if (currentCanvas != UICanvas)
            {
                SwitchCanvas(UICanvas, CurrentCanvas);
            }
        }

        if (winCondition.playerWin)
        {
            SwitchCanvas(winCanvas, UICanvas);
        }
        
        if(winCondition.playerLose)
        {
            SwitchCanvas(loseCanvas, UICanvas);
        }
    }

    private void SetAim()
    {
        if (playerCamera.IsAiming)
        {
            cross.SetActive(true);
        }
        else
        {
            cross.SetActive(false);
        }
    }
    private void SetHealthBar()
    {
        healthSliderPlayer.value = healthPlayer.CurrentHealth;

        if (isBoss)
        {
            healthSliderBoss.value = healthBoss.CurrentHealth;
            healthBossNumber.text = healthBoss.CurrentHealth + " / " + healthBoss.MaxHealth;
        }
    }
    private void SetWeaponSelect()
    {
        for (int i = 0; i < playerShoot.TotalWeapons; i++)
        {
            if (i == playerShoot.WeaponInUse)
            {
                weaponImage[i].color = Color.yellow;
            }
            else
            {
                weaponImage[i].color = Color.black;
            }
        }
    }

    private void SetTankShow()
    {
        if (!isBoss)
        {
            totalTanks = 0;
            for (int i = 0; i < tanks.Length; i++)
            {
                if (tanks[i] != null)
                {
                    totalTanks++;
                }
            }
            tankInfo.text = totalTanks.ToString();
        }
    }

    public void SwitchCanvas(CanvasGroup canvasEnable, CanvasGroup canvasDisable)
    {
        EnableCanvas(canvasEnable);
        DisableCanvas(canvasDisable);
        CurrentCanvas = canvasEnable;
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
