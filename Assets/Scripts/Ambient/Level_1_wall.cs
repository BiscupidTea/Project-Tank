using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_wall : MonoBehaviour
{
    [SerializeField] private GameObject tank1;
    [SerializeField] private GameObject tank2;
    [SerializeField] private GameObject tank3;
    [SerializeField] private GameObject tank4;
    [SerializeField] private GameObject tank5;
    [SerializeField] private GameObject tank6;
    private void Update()
    {
        if (tank1 == null && tank2 == null && tank3 == null && tank4 == null && tank5 == null && tank6 == null)
        {
            Destroy(gameObject);
        }
    }
}
