using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIlogic : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas canvasUI;

    [Header("Aim Info")]
    [SerializeField] private PlayerCameraController playerCameraControler;
    [SerializeField] private GameObject cross;

    [Header("HealthBar Info")]
    [SerializeField] private Player_Health healthPlayer;
    [SerializeField] private Enemy_Health healthBoss;
    [SerializeField] private TextMeshProUGUI healthBossNumber;
    [SerializeField] private bool isBoss;
    [SerializeField] private Slider healthSliderPlayer;
    [SerializeField] private Slider healthSliderBoss;

    [Header("Weapons Info")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Image Weapon1;
    [SerializeField] private Image Weapon2;

    [Header("TankRemaing Info")]
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private TextMeshProUGUI tankInfo;
    private int totTanks;

    [Header("Pause Info")]
    [SerializeField] private PauseSyestem pause;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraControler = player.GetComponent<PlayerCameraController>();
        playerShoot = player.GetComponent<PlayerShoot>();
        healthPlayer = player.GetComponent<Player_Health>();

        healthSliderPlayer.maxValue = healthPlayer.GetHealth();
        healthSliderPlayer.value = healthPlayer.GetHealth();

        if (isBoss)
        {
            healthSliderBoss.maxValue = healthBoss.GetHealth();
            healthSliderBoss.value = healthBoss.GetHealth();
        }

        for (int i = 0; i < tanks.Length; i++)
        {
            totTanks++;
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
        if (playerCameraControler.IsAiming())
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
        healthSliderPlayer.value = healthPlayer.GetHealth();

        if (isBoss)
        {
            healthSliderBoss.value = healthBoss.GetHealth();
            healthBossNumber.text = healthBoss.GetHealth() + " / " + healthBoss.GetMaxHealth();
        }
    }
    private void SetWeaponSelect()
    {
        if (playerShoot.WeaponUsing())
        {
            Weapon1.color = Color.yellow;
            Weapon2.color = Color.white;
        }
        else
        {
            Weapon1.color = Color.white;
            Weapon2.color = Color.yellow;
        }
    }

    private void SetTankShow()
    {
        if (!isBoss)
        {
            totTanks = 0;
            for (int i = 0; i < tanks.Length; i++)
            {
                if (tanks[i] != null)
                {
                    totTanks++;
                }
            }
            tankInfo.text = totTanks.ToString();
        }
    }
}
