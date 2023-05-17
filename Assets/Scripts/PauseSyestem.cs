using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSyestem : MonoBehaviour
{
    [SerializeField] private bool isPause;
    [SerializeField] private Canvas canvasPause;

    private void Start()
    {
        canvasPause.enabled = false;
    }

    private void Update()
    {
        if (isPause)
        {
            Time.timeScale = 0;
            canvasPause.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            canvasPause.enabled = false;
        }
        Debug.Log(isPause);
    }

    public void ChangePause(InputAction.CallbackContext input)
    {
        isPause = !isPause;
    }
}
