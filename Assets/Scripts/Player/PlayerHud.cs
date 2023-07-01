using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas canvasUI;

    [Header("Aim Info")]
    [SerializeField] private PlayerCameraController playerCameraController;
    [SerializeField] private GameObject cross;

    [Header("HealthBar Info")]
    [SerializeField] private Health healthPlayer;
    [SerializeField] private Health healthBoss;
    [SerializeField] private TextMeshProUGUI healthBossNumber;
    [SerializeField] private bool isBoss;
    [SerializeField] private Slider healthSliderPlayer;
    [SerializeField] private Slider healthSliderBoss;

    [Header("Weapons Info")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Image[] weaponImage;

    [Header("TankRemaing Info")]
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private TextMeshProUGUI tankInfo;
    private int totalTanks;

    [Header("Pause Info")]
    [SerializeField] private PauseSyestem pause;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraController = player.GetComponent<PlayerCameraController>();
        playerShoot = player.GetComponent<PlayerShoot>();
        healthPlayer = player.GetComponent<Health>();

        healthSliderPlayer.maxValue = healthPlayer.CurrentHealth;
        healthSliderPlayer.value = healthPlayer.CurrentHealth;

        if (isBoss)
        {
            healthSliderBoss.maxValue = healthBoss.CurrentHealth;
            healthSliderBoss.value = healthBoss.CurrentHealth;
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

        if (pause.returnIsPaused())
        {
            canvasUI.enabled = false;
        }
        else
        {
            canvasUI.enabled = true;
        }
    }

    private void SetAim()
    {
        if (playerCameraController.IsAiming())
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
                weaponImage[i].color = Color.white;
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
}
