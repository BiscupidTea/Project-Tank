using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIlogic : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Aim Info")]
    [SerializeField] private PlayerCameraController playerCameraControler;
    [SerializeField] private GameObject cross;

    [Header("HealthBar Info")]
    [SerializeField] private Player_Health healthPlayer;
    [SerializeField] private Slider healthSlider;

    [Header("Weapons Info")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Image Weapon1;
    [SerializeField] private Image Weapon2;

    [Header("TankRemaing Info")]
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private TextMeshProUGUI tankInfo;
    private int totTanks;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraControler = player.GetComponent<PlayerCameraController>();
        playerShoot = player.GetComponent<PlayerShoot>();
        healthPlayer = player.GetComponent<Player_Health>();

        healthSlider.maxValue = healthPlayer.GetHealth();
        healthSlider.value = healthPlayer.GetHealth();

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
        healthSlider.value = healthPlayer.GetHealth();
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
