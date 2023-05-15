using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraControler = player.GetComponent<PlayerCameraController>();
        healthPlayer = player.GetComponent<Player_Health>();

        healthSlider.maxValue = healthPlayer.GetHealth();
        healthSlider.value = healthPlayer.GetHealth();
    }
    void Update()
    {
        SetAim();

        SetHealthBar();
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
}
