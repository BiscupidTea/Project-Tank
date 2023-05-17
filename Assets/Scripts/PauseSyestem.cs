using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseSyestem : MonoBehaviour
{
    [SerializeField] private bool isPause;
    [SerializeField] private CanvasGroup canvasPause;

    private void Start()
    {
        isPause = false;

        canvasPause.alpha = isPause ? 1 : 0;
        canvasPause.blocksRaycasts = isPause;
        canvasPause.interactable = isPause;
    }

    public void ChangePause(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            isPause = !isPause;
            canvasPause.alpha = isPause ? 1 : 0;
            canvasPause.blocksRaycasts = isPause;
            canvasPause.interactable = isPause;

            if (isPause)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }

    public void ChangePause()
    {
        isPause = !isPause;
        canvasPause.alpha = isPause ? 1 : 0;
        canvasPause.blocksRaycasts = isPause;
        canvasPause.interactable = isPause;

        if (isPause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public bool returnIsPaused()
    {
        return isPause;
    }
}
