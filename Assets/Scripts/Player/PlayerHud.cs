using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas canvasUI;

    [Header("Aim Info")]
    [SerializeField] private PlayerCamera playerCamera;
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
    [SerializeField] private Image tankSprite;
    [SerializeField] private TextMeshProUGUI tankInfo;
    private int totalTanks;

    [Header("Pause Info")]
    [SerializeField] private PauseSystem pause;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.GetComponent<PlayerCamera>();
        playerShoot = player.GetComponent<PlayerShoot>();
        healthPlayer = player.GetComponent<Health>();

        healthSliderPlayer.maxValue = healthPlayer.CurrentHealth;
        healthSliderPlayer.value = healthPlayer.CurrentHealth;

        if (isBoss)
        {
            tankSprite.enabled = false;
            tankInfo.enabled = false;
            healthSliderBoss.enabled = true;
            healthSliderBoss.maxValue = healthBoss.CurrentHealth;
            healthSliderBoss.value = healthBoss.CurrentHealth;
        }
        else
        {
            tankSprite.enabled = true;
            tankInfo.enabled = true;
            healthSliderBoss.enabled = false;
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
            canvasUI.enabled = false;
        }
        else
        {
            canvasUI.enabled = true;
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
}
