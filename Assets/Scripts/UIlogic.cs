using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraControler = player.GetComponent<PlayerCameraController>();
        playerShoot = player.GetComponent<PlayerShoot>();
        healthPlayer = player.GetComponent<Player_Health>();

        healthSlider.maxValue = healthPlayer.GetHealth();
        healthSlider.value = healthPlayer.GetHealth();
    }
    void Update()
    {
        SetAim();

        SetHealthBar();

        SetWeaponSelect();
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
}
