using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIlogic : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShootStats;

    [SerializeField] private Image PrimaryShoot;

    [SerializeField] private Material ReadyShoot;
    [SerializeField] private Material NotReadyShoot;

    void Update()
    {
        if (playerShootStats.GetPrimaryReadyToShoot())
        {
            PrimaryShoot.color = Color.green;
        }
        else
        {
            PrimaryShoot.color = Color.red;
        }
    }
}
