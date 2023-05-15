using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIlogic : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerCameraController playerCameraControler;
    [SerializeField] private GameObject cross;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCameraControler = player.GetComponent<PlayerCameraController>();
    }
    void Update()
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
}
